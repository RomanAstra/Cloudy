using UnityEngine;

namespace Cloudy.Configs
{
    [CreateAssetMenu(menuName = "Upgrade/StatConfig", fileName = "New StatConfig")]
    public sealed class StatConfig : ScriptableObject
    {
        public Stat[] Stats;
    }
}