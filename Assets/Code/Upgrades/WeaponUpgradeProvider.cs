using System;
using System.Collections.Generic;
using Cloudy.Configs;
using Random = UnityEngine.Random;

namespace Configs.Upgrades.Weapons
{
    public sealed class WeaponUpgradeProvider
    {
        private readonly Dictionary<string, List<StatConfig>> _configsMap = new();
        private readonly Dictionary<string, List<StatConfig>> _tempMap = new();
        private readonly Dictionary<string, List<StatConfig>> _providedConfigs = new();
        
        public WeaponUpgradeProvider(AllWeaponsUpgradeConfig weaponUpgradeConfigs)
        {
            foreach (var configs in weaponUpgradeConfigs.ConfigsMap)
            {
                _configsMap[configs.Key] = new List<StatConfig>(configs.Value.Configs);
            }
            
            Reset();
        }
        
        public StatConfig GetConfig(string weaponType)
        {
            if (!_tempMap.ContainsKey(weaponType))
                throw new InvalidOperationException($"Configs with {weaponType} type not exist");
            
            var configs = _tempMap[weaponType];

            if (configs.Count == 0)
                throw new InvalidOperationException($"Configs with {weaponType} type has empty");
            
            var index = Random.Range(0, configs.Count);
            var config = configs[index];
            configs.RemoveAt(index);
          
            if(!_providedConfigs.ContainsKey(weaponType))
                _providedConfigs.Add(weaponType, new List<StatConfig>());
            
            _providedConfigs[weaponType].Add(config);
            
            return config;
        }
        public void RemoveConfig(StatConfig config)
        {
            foreach (var configs in _providedConfigs)
            {
                for (var i = 0; i < configs.Value.Count; i++)
                {
                    var value = configs.Value[i];
                    
                    if (value != config)
                        _tempMap[configs.Key].Add(value);
                }

                configs.Value.Clear();
            }
        }
        public void Reset()
        {
            foreach (var configs in _configsMap)
            {
                _tempMap[configs.Key] = new List<StatConfig>(configs.Value);
            }
        }
    }
}