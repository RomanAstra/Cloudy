using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cloudy
{
    public sealed class WeaponsDataProvider
    {
        private readonly string[] _weapons;
        private readonly List<string> _currentWeapons = new();
        private readonly OpenWeaponsPrefsController _openWeaponsIndexPrefsController;
            
        public WeaponsDataProvider(string[] weapons, OpenWeaponsPrefsController openWeaponsIndexPrefsController)
        {
            _weapons = weapons;
            _openWeaponsIndexPrefsController = openWeaponsIndexPrefsController;
        }
        
        public string[] GetWeapons()
        {
            return _weapons.ToArray();
        }
        public string GetWeapon(int index)
        {
            return _weapons[index];
        }
        public string[] GetCurrentWeapons()
        {
            return _currentWeapons.ToArray();
        }
        public string[] GetRandomWeapons(int count)
        {
            RollWeapons(count);
            return _currentWeapons.ToArray();
        }

        private void RollWeapons(int count)
        {
            _currentWeapons.Clear();
            var openCount = _openWeaponsIndexPrefsController.GetCount();
            var tempWeapons = new List<string>();

            for (var i = 0; i < openCount; i++)
            {
                tempWeapons.Add(_weapons[i]);
            }

            var length = openCount < count ? openCount : count;
            if (length <= count)
            {
                _currentWeapons.AddRange(tempWeapons);
                return;
            }
            
            for (var i = 0; i < length; i++)
            {
                var index = Random.Range(0, tempWeapons.Count);
                var weapon = tempWeapons[index];
                _currentWeapons.Add(weapon);
                tempWeapons.RemoveAt(index);
            }
        }
    }
}