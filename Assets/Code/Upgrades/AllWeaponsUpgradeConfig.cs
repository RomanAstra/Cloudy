using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Configs.Upgrades.Weapons
{
    [CreateAssetMenu(menuName = "Upgrade/AllWeaponsUpgradeConfig", fileName = "AllWeaponsUpgradeConfig")]
    public sealed class AllWeaponsUpgradeConfig : ScriptableObject
    {
        [SerializeField] private WeaponUpgradeConfig[] _configs;

        public IReadOnlyDictionary<string, WeaponUpgradeConfig> ConfigsMap;

        private void OnValidate()
        {
            ConfigsMap = _configs.ToDictionary(config => config.Type.ToString());
        }
    }
}