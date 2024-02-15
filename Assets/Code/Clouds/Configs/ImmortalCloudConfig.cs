using UnityEngine;

namespace Cloudy.Configs
{
    [CreateAssetMenu(menuName = "Configs/Clouds/Immortal", fileName = nameof(ImmortalCloudConfig))]
    public class ImmortalCloudConfig : CloudConfig
    {
        public float ShieldTime = 4f;
        public float ShieldDelay = 3f;
        public float ShieldAlpha = 0.5f;
        public float ShieldBlinkDelay = 0.2f;
    }
}