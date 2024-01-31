using UnityEngine;
using Utils;

namespace Cloudy
{
    public sealed class BulletSpawner
    {
        private readonly Pool<Bullet> _pool;
        private readonly Transform _parent;

        public BulletSpawner(Bullet bulletPrefab, Transform parent)
        {
            _pool = new Pool<Bullet>(bulletPrefab);
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