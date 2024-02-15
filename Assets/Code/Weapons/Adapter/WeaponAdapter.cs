using Cloudy.Configs;
using UnityEngine;
using Utils;

namespace Cloudy
{
    public class WeaponAdapter : IUpdate
    {
        private readonly WeaponHierarchy _hierarchy;
        private readonly IConcreteBulletPool _bulletPool;
        private readonly Transform _firePoint;
        private readonly float _inaccuracy;
        protected readonly Countdown _fireDelay;
        private bool _isEnable;
        
        public WeaponAdapter(WeaponHierarchy hierarchy, IConcreteBulletPool bulletPool, WeaponUpgradeSystem weaponUpgradeSystem,
            WeaponConfig config, string id)
        {
            _hierarchy = hierarchy;
            _bulletPool = bulletPool;
            _fireDelay = new Countdown(config.FireDelay);
            _firePoint = hierarchy.FirePoint;
            _inaccuracy = config.Inaccuracy;
            
            _fireDelay.Duration += weaponUpgradeSystem.GetValue(id, StatType.Cooldown);
        }

        public void OnUpdate(float deltaTime)
        {
            if(!_isEnable)
                return;
            
            _fireDelay.Update();
        }
        public virtual void Fire()
        { 
            if(!_fireDelay.IsEnded)
                return;

            _fireDelay.Reset();
            SpawnBullet();
        }
        public void Show()
        {
            _isEnable = true;
            _hierarchy.Show();
        }
        public void Hide()
        {
            _isEnable = false;
            _hierarchy.Hide();
        }

        protected void SpawnBullet()
        {
            var direction = _firePoint.right + new Vector3(Random.Range(-_inaccuracy, _inaccuracy), Random.Range(-_inaccuracy, _inaccuracy), 0f);
            _bulletPool.Spawn(_firePoint.position, _firePoint.rotation, direction);
        }
    }
}