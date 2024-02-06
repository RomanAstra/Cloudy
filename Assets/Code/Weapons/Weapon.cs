using Cloudy.Configs;
using Configs.Upgrades;
using UnityEngine;

namespace Cloudy
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] protected Transform _firePoint;
        [SerializeField] protected Countdown _fireDelay;
        [SerializeField] protected WeaponType _weaponType;

        protected BulletSpawner _bulletSpawner;
        
        protected virtual void Awake()
        {
            _bulletSpawner = new BulletSpawner(_bulletPrefab, null);
            _fireDelay.Duration += WeaponUpgradeSystem.GetValue(_weaponType, StatType.Cooldown);
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
