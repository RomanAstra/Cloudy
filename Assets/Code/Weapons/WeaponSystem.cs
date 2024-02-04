using System;
using JetBrains.Annotations;
using UnityEngine;
using Utils;

namespace Cloudy
{
    [UsedImplicitly]
    public sealed class WeaponSystem : IInitializer, IDisposable
    {
        private readonly IFireInput _fireInput;
        private readonly IChangeWeaponInput _changeWeapon;
        private readonly Transform _container;
        private readonly WeaponsProvider _weaponsProvider;
        private Weapon[] _weapons;
        private int _weaponIndex = -1;

        public WeaponSystem(IFireInput fireInput, IChangeWeaponInput changeWeapon, Transform container)
        {
            _fireInput = fireInput;
            _changeWeapon = changeWeapon;
            _container = container;
            
            _fireInput.OnFired += OnFired;
            _changeWeapon.OnChanged += OnWeaponChanged;

            _weaponsProvider = new WeaponsProvider();
        }
        public async void OnStart()
        {
            _weapons = await _weaponsProvider.InstantiateAsync(App.CurrentWeapons, _container);
            OnWeaponChanged();
        }
        public void Dispose()
        {
            _fireInput.OnFired -= OnFired;
            _changeWeapon.OnChanged -= OnWeaponChanged;
        }

        private void OnFired()
        {
            _weapons[_weaponIndex].Fire();
        }
        private void OnWeaponChanged()
        {
            if(_weapons.Length == 0)
                return;
            
            if(_weaponIndex >= 0)
                _weapons[_weaponIndex].Hide();
                
            _weaponIndex = ++_weaponIndex % _weapons.Length;
            _weapons[_weaponIndex].Show();
        }
    }
}