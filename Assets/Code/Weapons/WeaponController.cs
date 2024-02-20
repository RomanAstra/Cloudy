using Utils;

namespace Cloudy
{
    public sealed class WeaponController : IGameStart, IGameFinish, IGamePause, IGameResume
    {
        private readonly IFireInput _fireInput;
        private readonly IChangeWeaponInput _changeWeapon;
        private readonly WeaponsProvider _weaponsProvider;
        
        public WeaponAdapter CurrentWeaponAdapter { get; private set; }

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
            CurrentWeaponAdapter.Fire();
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