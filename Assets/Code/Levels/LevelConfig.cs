using UnityEngine;

namespace Cloudy
{
    [CreateAssetMenu(menuName = "Configs/Level", fileName = "Location?Level?")]
    public sealed class LevelConfig : ScriptableObject
    {
        public ZoneSettings[] ZonesSettings;
    }
}