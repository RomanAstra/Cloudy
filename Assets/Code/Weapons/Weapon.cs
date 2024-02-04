using UnityEngine;

namespace Cloudy
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] protected Transform _firePoint;
        [SerializeField] protected Countdown _fireDelay;

        protected BulletSpawner _bulletSpawner;

        private void Awake()
        {
            _bulletSpawner = new BulletSpawner(_bulletPrefab, null);
        }
        private void Update()
        {
            _fireDelay.Update();
        }

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
            _bulletSpawner.Spawn(_firePoint.position, _firePoint.rotation, _firePoint.right);
        }
    }
}
