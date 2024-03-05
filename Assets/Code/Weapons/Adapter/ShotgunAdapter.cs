using Cloudy.Configs;
using Configs.Upgrades;
using UnityEngine;

namespace Cloudy
{
    public sealed class ShotgunAdapter : WeaponAdapter
    {
        private readonly int _bulletsCount;
        
        public ShotgunAdapter(WeaponHierarchy hierarchy, IConcreteBulletPool bulletPool, WeaponUpgradeSystem weaponUpgradeSystem,
            ShotgunConfig config, string id) : base(hierarchy, bulletPool, weaponUpgradeSystem, config, id)
        {
            _bulletsCount = config.BulletsCount;

            _bulletsCount += (int)weaponUpgradeSystem.GetValue(id, StatType.Bullets);
        }
        
        protected override void SpawnBullet()
        {
            for (var i = 0; i < _bulletsCount; i++)
            {
                base.SpawnBullet();
            }
        }
    }
}