using System.Collections.Generic;
using System.Linq;
using Cloudy.SaveData;
using UnityEngine;

namespace Cloudy
{
    public sealed class WeaponsDataProvider
    {
        private readonly IReadOnlyList<OpenObjectStarsData> _weapons;
        private readonly SaveSystem _saveSystem;
        private readonly List<string> _currentWeapons = new();
        private readonly Dictionary<string, OpenObjectStarsData> _openedWeapons = new();
            
        public WeaponsDataProvider(IReadOnlyList<OpenObjectStarsData> weapons, SaveSystem saveSystem)
        {
            _weapons = weapons;
            _saveSystem = saveSystem;
            
            _saveSystem.OnLoadData += OnLoadSave;
        }
        
        public IReadOnlyList<OpenObjectStarsData> GetWeapons()
        {
            return _weapons.ToArray();
        }
        public IReadOnlyList<string> GetCurrentWeapons()
        {
            return _currentWeapons;
        }
        public bool CheckOpenedWeapons(string weaponId)
        {
            return _openedWeapons.ContainsKey(weaponId);
        }
        public IReadOnlyList<string> GetRandomWeapons(int count)
        {
            RollWeapons(count);
            return _currentWeapons;
        }
        public void AddOpenWeapon(OpenObjectStarsData weapon)
        {
            _openedWeapons.Add(weapon.Id, weapon);
        }

        private void RollWeapons(int count)
        {
            _currentWeapons.Clear();
            var openCount = _openedWeapons.Count;
            var tempWeapons = new List<string>();
            tempWeapons.AddRange(_openedWeapons.Keys);

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
        private void OnLoadSave(SaveData.SaveData saveData)
        {
            var starsCount = _saveSystem.SaveData.GetStarsCount();
            for (var i = 0; i < _weapons.Count; i++)
            {
                var weapon = _weapons[i];
                
                if(weapon.StarsCount <= starsCount)
                    _openedWeapons.Add(weapon.Id, weapon);
            }
            
            _saveSystem.OnLoadData -= OnLoadSave;
        }
    }
}