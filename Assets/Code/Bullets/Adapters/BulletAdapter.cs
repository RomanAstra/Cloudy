using System;
using Cloudy.Configs;
using UnityEngine;
using Utils;

namespace Cloudy
{
    public class BulletAdapter : IUpdate, IGameFinish, 
    IGamePause, IGameResume
    {
        protected readonly BulletHierarchy _hierarchy;
        protected readonly string _id;
        private readonly BulletConfig _config;
        private readonly RigidbodyStateController _rigidbodyController;
        private Countdown _lifeTime;
        private Vector3 _moveDirection;
        private int _currentRicochetCount;
        private int _currentTargetCount;
        private int _damage;
        private float _speed;
        private int _ricochetCount;
        private int _targetCount;

        public event Action<BulletAdapter, BulletHierarchy> OnRelease;
        
        public void OnUpdate(float deltaTime)
        {
            if (_lifeTime.IsInfinitely)
                return;

            _lifeTime.Update();

            if (_lifeTime.IsEnded)
                Release();
        }
        public BulletAdapter(BulletHierarchy hierarchy, WeaponUpgradeSystem weaponUpgradeSystem, BulletConfig config, string id)
        {
            _hierarchy = hierarchy;
            _config = config;
            _id = id;
            _rigidbodyController = new RigidbodyStateController(_hierarchy.Rigidbody);

            _hierarchy.onCollisionEnter += OnCollisionEnter;
            _hierarchy.onTriggerEnter += OnTriggerEnter;
            
            SetUpgrades(weaponUpgradeSystem);
            
            _currentRicochetCount = 0;
            _currentTargetCount = 0;
            _lifeTime.Reset();
        }
        public void Move(Vector3 direction)
        {
            _hierarchy.Rigidbody.velocity = _speed * direction;
        }
        public void OnFinish()
        {
            _rigidbodyController.OnFinish();
        }
        public void OnPause()
        {
            _rigidbodyController.OnPause();
        }
        public void OnResume()
        {
            _rigidbodyController.OnResume();
        }
        
        private void OnCollisionEnter(Collision obj)
        {
            if(_ricochetCount < 0)
                return;
            
            _currentRicochetCount++;

            if (_currentRicochetCount > _ricochetCount)
                Release();
        }
        private void OnTriggerEnter(CloudHierarchy cloud)
        {
            _currentTargetCount++;
            cloud.DealDamage(_damage);

            if (_targetCount >= 0 && _currentTargetCount >= _targetCount)
                Release();
        }
        private void SetUpgrades(WeaponUpgradeSystem weaponUpgradeSystem)
        {
            _damage = _config.Damage + (int)weaponUpgradeSystem.GetValue(_id, StatType.Damage);
            _speed = _config.Speed + weaponUpgradeSystem.GetValue(_id, StatType.Speed);
            _ricochetCount = _config.RicochetCount + (int)weaponUpgradeSystem.GetValue(_id, StatType.Ricochets);
            _targetCount = _config.TargetCount + (int)weaponUpgradeSystem.GetValue(_id, StatType.Targets);
            
            var scale = weaponUpgradeSystem.GetValue(_id, StatType.Scale);
            _hierarchy.transform.localScale += new Vector3(scale, scale);
            
            var lifeTime = _config.LifeTime + weaponUpgradeSystem.GetValue(_id, StatType.LifeTime);
            
            _lifeTime = new Countdown(lifeTime, lifeTime);
        }
        protected virtual void Release()
        {
            _hierarchy.onCollisionEnter -= OnCollisionEnter;
            _hierarchy.onTriggerEnter -= OnTriggerEnter;
            
            OnRelease?.Invoke(this, _hierarchy);
        }
    }
}