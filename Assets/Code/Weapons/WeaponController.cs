using UnityEngine;
using Utils;

namespace Cloudy
{
    public sealed class WeaponController : IGameStart, IGameFinish, IGamePause, IGameResume
    {
        private readonly IFireInput _fireInput;
        private readonly IChangeWeaponInput _changeWeapon;
        private readonly WeaponsProvider _weaponsProvider;
        private readonly AudioSource _audioSource;

        public WeaponAdapter CurrentWeaponAdapter { get; private set; }

        public WeaponController(IFireInput fireInput, IChangeWeaponInput changeWeapon, 
            WeaponsProvider weaponsProvider, AudioSource audioSource)
        {
            _fireInput = fireInput;
            _changeWeapon = changeWeapon;
            _weaponsProvider = weaponsProvider;
            _audioSource = audioSource;
        }
        void IGameStart.OnStart()
        {
            Subscribe();
            OnWeaponChanged();
        }
        void IGameFinish.OnFinish()
        {
            Unsubscribe();
        }
        void IGamePause.OnPause()
        {
            Unsubscribe();
        }
        void IGameResume.OnResume()
        {
            Subscribe();
        }

        private void OnFired()
        {
            if(CurrentWeaponAdapter.Fire())
                _audioSource.PlayOneShot(CurrentWeaponAdapter.FireAudioClip);
        }
        private void OnWeaponChanged()
        {
            CurrentWeaponAdapter?.Hide();
            CurrentWeaponAdapter = _weaponsProvider.GetNextWeapon();
            CurrentWeaponAdapter.Show();
        }
        private void Subscribe()
        {
            _fireInput.OnFired += OnFired;
            _changeWeapon.OnChanged += OnWeaponChanged;
        }
        private void Unsubscribe()
        {
            _fireInput.OnFired -= OnFired;
            _changeWeapon.OnChanged -= OnWeaponChanged;
        }
    }
}