using UnityEngine;

namespace Cloudy.Configs
{
    public abstract class BulletConfig : ScriptableObject
    {
        public int damage;
        public float speed;
        public int ricochetCount;
        public int targetCount;
        public float lifeTime;
    }
}