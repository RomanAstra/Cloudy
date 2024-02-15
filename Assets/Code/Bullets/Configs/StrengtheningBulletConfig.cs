using UnityEngine;

namespace Cloudy.Configs
{
    [CreateAssetMenu(menuName = "Configs/Bullets/Strengthening", fileName = nameof(StrengtheningBulletConfig))]
    public sealed class StrengtheningBulletConfig : BulletConfig
    {
        public int Strengthening = 1;
    }
}