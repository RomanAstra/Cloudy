using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Configs.Upgrades.Weapons
{
    [CreateAssetMenu(menuName = "Upgrade/AllWeaponsUpgradeConfig", fileName = "AllWeaponsUpgradeConfig")]
    public sealed class AllWeaponsUpgradeConfig : ScriptableObject
    {
        [SerializeField] private WeaponUpgradeConfig[] _configs;

        private Dictionary<string, WeaponUpgradeConfig> _configsMap;

        private void OnValidate()
        {
            _configsMap = _configs.ToDictionary(config => config.Type.ToString());
        }

        public WeaponUpgradeConfig GetConfig(string type)
        {
            if (!_configsMap.ContainsKey(type))
                return default;
            
            return _configsMap[type];
        } 
    }
}