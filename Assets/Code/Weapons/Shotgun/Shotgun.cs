using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class Shotgun : Weapon
    {
        [SerializeField] private int _bulletsCount = 10;
        [SerializeField] private float _inaccuracy = 10f;
        
        [Inject]
        public void Construct(ShotgunBulletSpawner bulletSpawner)
        {
            base.Construct(bulletSpawner);
        }
        public override void Fire()
        {
            if(!_fireDelay.IsEnded)
                return;
            
            _fireDelay.Reset();
            for (var i = 0; i < _bulletsCount; i++)
            {
                var position = _firePoint.position + new Vector3(Random.Range(-_inaccuracy, _inaccuracy), Random.Range(-_inaccuracy, _inaccuracy));
                var direction = _firePoint.right + new Vector3(0f, Random.Range(-_inaccuracy, _inaccuracy), 0f);
                _bulletSpawner.Spawn(position, _firePoint.rotation, direction);
            }
        }
    }
}