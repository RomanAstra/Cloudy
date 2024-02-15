using UnityEngine;

namespace Cloudy.Configs
{
    [CreateAssetMenu(menuName = "Configs/Clouds/Moving", fileName = nameof(MovingCloudConfig))]
    public class MovingCloudConfig : CloudConfig
    {
        public float Duration = 1f;
    }
}