using UnityEngine;
using Utils;

namespace Cloudy
{
    public abstract class BulletSpawner
    {
        private readonly IBulletPool<Bullet> _pool;
        private readonly Transform _parent;

        protected BulletSpawner(IBulletPool<Bullet> pool, Transform parent)
        {
            _pool = pool;
            _parent = parent;
        }

        public void Spawn(Vector3 position, Quaternion rotation, Vector3 moveDirection)
        {
            var bullet = _pool.Get(position, rotation, _parent);
            bullet.SetPool(_pool);
            bullet.Move(moveDirection);
        }
    }
}