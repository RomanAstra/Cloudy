using System.Collections.Generic;
using Cloudy.Configs;

namespace Cloudy
{
    public sealed class UpgradeWeapon
    {
        private readonly Dictionary<StatType, float> _values = new ();

        public void AddValue(StatType statType, float value)
        {
            _values.TryAdd(statType, 0f);

            _values[statType] += value;
        }
        public float GetValue(StatType statType)
        {
            if (!_values.ContainsKey(statType))
                return 0f;
            
            return _values[statType];
        }
    }
}