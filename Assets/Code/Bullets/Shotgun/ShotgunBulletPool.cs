using UnityEngine;
using Utils;

namespace Cloudy
{
    public sealed class ShotgunBulletPool : IBulletPool<Bullet>
    {
        private readonly Pool<Bullet> _pool;
        
        public ShotgunBulletPool(Bullet prefab)
        {
            _pool = new Pool<Bullet>(prefab);
        }

        public Bullet Get(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            return _pool.Get(position, rotation, parent);
        }
        public void Release(Bullet poolObject)
        {
            _pool.Release(poolObject);
        }
    }
}