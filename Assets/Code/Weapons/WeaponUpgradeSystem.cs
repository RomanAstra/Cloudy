using System.Collections.Generic;
using Cloudy.Configs;

namespace Cloudy
{
    public sealed class WeaponUpgradeSystem
    {
        private readonly Dictionary<string, UpgradeWeapon> _configs = new ();

        public void AddUpgrade(string weaponType, StatType statType, float value)
        {
            _configs.TryAdd(weaponType, new UpgradeWeapon());

            _configs[weaponType].AddValue(statType, value);
        }
        public float GetValue(string weaponType, StatType statType)
        {
            if (!_configs.ContainsKey(weaponType))
                return 0f;

            return _configs[weaponType].GetValue(statType);
        }
        public void Reset()
        {
            _configs.Clear();
        }
    }
}