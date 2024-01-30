using UnityEngine;
using Utils;

namespace Cloudy
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] protected Transform _firePoint;
        [SerializeField] protected Countdown _fireDelay;

        //protected BulletSpawner _bulletSpawner;
        protected Pool<Bullet> _pool;

        private void Awake()
        {
            _pool = new Pool<Bullet>(_bulletPrefab);
        }
        private void Update()
        {
            _fireDelay.Update();
        }

        // protected void Construct(BulletSpawner bulletSpawner)
        // {
        //     _bulletSpawner = bulletSpawner;
        // }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public virtual void Fire()
        { 
            if(!_fireDelay.IsEnded)
                return;

            _fireDelay.Reset();
            //_bulletSpawner.Spawn(_firePoint.position, _firePoint.rotation, _firePoint.right);
            var bullet = _pool.Get(_firePoint.position, _firePoint.rotation);
            bullet.SetPool(_pool);
            bullet.Move(_firePoint.right);
        }
    }
}
