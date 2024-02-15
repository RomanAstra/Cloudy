using System.Collections.Generic;
using Cloudy.Configs;
using UnityEngine;

 namespace Configs.Upgrades.Weapons
{
    [CreateAssetMenu(menuName = "Upgrade/WeaponUpgradeConfig", fileName = "New UpgradeConfig")]
    public sealed class WeaponUpgradeConfig : ScriptableObject
    {
        [SerializeField] private string _type;
        [SerializeField] private StatConfig[] _configs;

        public IReadOnlyCollection<StatConfig> Configs => _configs;
        
        public string Type => _type;
    }
}