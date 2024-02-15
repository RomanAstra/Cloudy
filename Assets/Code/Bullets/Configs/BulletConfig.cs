using UnityEngine;

namespace Cloudy.Configs
{
    [CreateAssetMenu(menuName = "Configs/Bullets/Bullet", fileName = nameof(BulletConfig))]
    public class BulletConfig : ScriptableObject
    {
        public int Damage = 1;
        public float Speed = 10f;
        public int RicochetCount;
        public int TargetCount;
        public float LifeTime = 4f;
    }
}