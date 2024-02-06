using System.Collections.Generic;
using Cloudy;
using Cloudy.Configs;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Configs.Upgrades.Weapons
{
    [CreateAssetMenu(menuName = "Upgrade/WeaponUpgradeConfig", fileName = "New UpgradeConfig")]
    public sealed class WeaponUpgradeConfig : ScriptableObject
    {
        [SerializeField] private WeaponType _type;
        [SerializeField] private StatConfig[] _configs;

        private readonly List<StatConfig> _tempConfigs = new ();

        public WeaponType Type => _type;
        
        private void OnValidate()
        {
            _tempConfigs.AddRange(_configs);
        }

        public StatConfig GetRandomConfig()
        {
            if (_tempConfigs.Count == 0)
                return default;
            
            return _tempConfigs[Random.Range(0, _tempConfigs.Count)];
        }
        public void Remove(StatConfig config)
        {
            _tempConfigs.Remove(config);
        }
    }
}