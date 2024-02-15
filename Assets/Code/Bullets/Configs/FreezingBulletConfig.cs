using UnityEngine;

namespace Cloudy.Configs
{
    [CreateAssetMenu(menuName = "Configs/Bullets/Freezing", fileName = nameof(FreezingBulletConfig))]
    public sealed class FreezingBulletConfig : BulletConfig
    {
        public float FreezingTime = 3f;
    }
}