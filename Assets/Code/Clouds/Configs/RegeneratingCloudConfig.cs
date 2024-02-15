using UnityEngine;

namespace Cloudy.Configs
{
    [CreateAssetMenu(menuName = "Configs/Clouds/Regenerating", fileName = nameof(RegeneratingCloudConfig))]
    public class RegeneratingCloudConfig : CloudConfig
    {
        public float RegeneratingDelay = 2f;
        public int HitPointsRegeneratingCount = 1;
    }
}