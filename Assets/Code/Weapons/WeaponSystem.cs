using System;
using JetBrains.Annotations;
using Utils;

namespace Cloudy
{
    [UsedImplicitly]
    public sealed class WeaponSystem : IInitializer, IDisposable
    {
        private readonly IFireInput _fireInput;
        private readonly IChangeWeaponInput _changeWeapon;
        private readonly Weapon[] _weapons;
        private int _weaponIndex = -1;

        public WeaponSystem(IFireInput fireInput, IChangeWeaponInput changeWeapon, Weapon[] weapons)
        {
            _fireInput = fireInput;
            _changeWeapon = changeWeapon;
            _weapons = weapons;
            
            _fireInput.OnFired += OnFired;
            _changeWeapon.OnChanged += OnWeaponChanged;
        }
        public void Dispose()
        {
            _fireInput.OnFired -= OnFired;
            _changeWeapon.OnChanged -= OnWeaponChanged;
        }
        public void OnStart()
        {
            OnWeaponChanged();
        }

        private void OnFired()
        {
            _weapons[_weaponIndex].Fire();
        }
        private void OnWeaponChanged()
        {
            if(_weaponIndex >= 0)
                _weapons[_weaponIndex].Hide();
                
            _weaponIndex = ++_weaponIndex % _weapons.Length;
            _weapons[_weaponIndex].Show();
        }
    }
}