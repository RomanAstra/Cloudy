using System.Collections.Generic;
using Cloudy.Configs;
using Configs.Upgrades;

namespace Cloudy
{
    public sealed class WeaponUpgradeSystem
    {
        private static readonly Dictionary<string, UpgradeWeapon> _configs = new ();

        public static void AddUpgrade(string weaponType, StatType statType, float value)
        {
            _configs.TryAdd(weaponType, new UpgradeWeapon());

            _configs[weaponType].AddValue(statType, value);
        }
        public static float GetValue(WeaponType weaponType, StatType statType)
        {
            var type = weaponType.ToString();
            if (!_configs.ContainsKey(type))
                return 0f;

            return _configs[type].GetValue(statType);
        }
        public static void Reset()
        {
            _configs.Clear();
        }
    }
}