using UnityEngine;

namespace Cloudy.Configs
{
    [CreateAssetMenu(menuName = "Configs/Clouds/Cloud", fileName = nameof(CloudConfig))]
    public class CloudConfig : ScriptableObject
    {
        public int HitPoints = 20;
        public float CaptureSpeed = 0.25f;
    }
}