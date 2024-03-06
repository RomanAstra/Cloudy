using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Cloudy
{
    public sealed class WeaponsProvider : IUpdate
    {
        private readonly WeaponFactory _weaponFactory;
        private readonly List<WeaponAdapter> _weaponAdapters = new();
        private int _weaponIndex = -1;

        public WeaponsProvider(WeaponFactory weaponFactory, WeaponsDataProvider dataWeaponsProvider)
        {
            _weaponFactory = weaponFactory;
            SpawnWeapons(dataWeaponsProvider.GetCurrentWeapons());
        }

        void IUpdate.OnUpdate(float deltaTime)
        {
            for (var i = 0; i < _weaponAdapters.Count; i++)
            {
                _weaponAdapters[i].OnUpdate(Time.deltaTime);
            }
        }
        public WeaponAdapter GetNextWeapon()
        {
            if (_weaponAdapters.Count == 0)
                throw new InvalidOperationException("Weapons is empty!");
                
            _weaponIndex = ++_weaponIndex % _weaponAdapters.Count;
            return _weaponAdapters[_weaponIndex];
        }
        
        private async void SpawnWeapons(IEnumerable<string> ids)
        {
            foreach (var weaponId in ids)
            {
                _weaponAdapters.Add(await _weaponFactory.Spawn(weaponId)) ;
            }
        }
    }
}