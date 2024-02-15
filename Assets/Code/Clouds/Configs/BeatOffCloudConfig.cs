using UnityEngine;

namespace Cloudy.Configs
{
    [CreateAssetMenu(menuName = "Configs/Clouds/BeatOff", fileName = nameof(BeatOffCloudConfig))]
    public class BeatOffCloudConfig : CloudConfig
    {
        public float ShieldDelay = 2;
    }
}