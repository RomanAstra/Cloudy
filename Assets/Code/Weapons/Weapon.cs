using UnityEngine;

namespace Cloudy
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform _firePoint;
        [SerializeField] protected Countdown _fireDelay;
        
        protected BulletSpawner _bulletSpawner;

        private void Update()
        {
            _fireDelay.Update();
        }

        protected void Construct(BulletSpawner bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
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
