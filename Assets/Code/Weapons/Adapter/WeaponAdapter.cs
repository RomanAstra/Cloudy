using Cloudy.Configs;
using UnityEngine;
using Utils;

namespace Cloudy
{
    public class WeaponAdapter : IUpdate
    {
        private readonly WeaponHierarchy _hierarchy;
        private readonly Transform _transform;
        private readonly IConcreteBulletPool _bulletPool;
        private readonly Transform _firePoint;
        private readonly ParticleSystem _fireParticle;
        private readonly float _inaccuracy;
        private readonly float _recoilPower;
        protected readonly Countdown _fireDelay;
        private bool _isEnable;

        public float RotationSpeed { get; private set; }
        public AudioClip FireAudioClip { get; private set; }
        
        public WeaponAdapter(WeaponHierarchy hierarchy, IConcreteBulletPool bulletPool, WeaponUpgradeSystem weaponUpgradeSystem,
            WeaponConfig config, string id)
        {
            _hierarchy = hierarchy;
            _transform = hierarchy.transform.parent;

            _firePoint = hierarchy.FirePoint;
            _fireParticle = hierarchy.FireParticles;
            FireAudioClip = hierarchy.FireAudioClip;

            _bulletPool = bulletPool;
            _inaccuracy = config.Inaccuracy;
            _recoilPower = config.RecoilPower;
            _fireDelay = new Countdown(config.FireDelay);

            RotationSpeed = config.RotationSpeed;

            _fireDelay.Duration += weaponUpgradeSystem.GetValue(id, StatType.Cooldown);
        }

        public void OnUpdate(float deltaTime)
        {
            if(!_isEnable)
                return;
            
            _fireDelay.Update();
        }
        public bool Fire()
        { 
            if(!_fireDelay.IsEnded)
                return false;

            _fireDelay.Reset();
            SpawnBullet();
            Recoil();
            _fireParticle.Play(true);
            
            return true;
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

        protected virtual void SpawnBullet()
        {
            var direction = _firePoint.right + new Vector3(Random.Range(-_inaccuracy, _inaccuracy), 
                Random.Range(-_inaccuracy, _inaccuracy), 0f);
            
            _bulletPool.Spawn(_firePoint.position, _firePoint.rotation, direction);
        }
        private void Recoil()
        {
            var z = Mathf.Abs(_transform.eulerAngles.z);
            var sign = z is > 90 and < 270 ? -1 : 1;

            _transform.localEulerAngles += new Vector3(0f, 0f, _recoilPower * sign);
        }
    }
}