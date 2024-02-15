using UnityEngine;
using Utils;

namespace Cloudy
{
    public sealed class WeaponController : IGameStart, IGameFinish, IGamePause, IGameResume
    {
        private readonly IFireInput _fireInput;
        private readonly IChangeWeaponInput _changeWeapon;
        private readonly WeaponsProvider _weaponsProvider;
        private WeaponAdapter _currentWeaponAdapter;
        
        public WeaponController(IFireInput fireInput, IChangeWeaponInput changeWeapon, WeaponsProvider weaponsProvider)
        {
            _fireInput = fireInput;
            _changeWeapon = changeWeapon;
            _weaponsProvider = weaponsProvider;
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
            _currentWeaponAdapter.Fire();
        }
        private void OnWeaponChanged()
        {
            _currentWeaponAdapter?.Hide();
            _currentWeaponAdapter = _weaponsProvider.GetNextWeapon();
            _currentWeaponAdapter.Show();
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