using Cloudy.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Cloudy
{
    public sealed class WeaponFactory
    {
        private readonly WeaponUpgradeSystem _weaponUpgradeSystem;
        private readonly Transform _weaponsParent;
        private readonly BulletPoolFactory _bulletPoolFactory;
        private readonly WeaponSpawner _weaponSpawner = new ();
        private readonly WeaponConfigLoader _weaponConfigLoader = new ();

        public WeaponFactory(WeaponUpgradeSystem weaponUpgradeSystem, Transform weaponsParent, BulletPoolFactory bulletPoolFactory)
        {
            _weaponUpgradeSystem = weaponUpgradeSystem;
            _weaponsParent = weaponsParent;
            _bulletPoolFactory = bulletPoolFactory;
        }

        public async UniTask<WeaponAdapter> Spawn(string id)
        {
            var hierarchy =  await _weaponSpawner.InstantiateAsync(id, _weaponsParent);
            var pool = await _bulletPoolFactory.GetBulletPool(id);

            switch (id)
            {
                default:
                    var config = await _weaponConfigLoader.LoadAsync<WeaponConfig>(id);
                    return new WeaponAdapter(hierarchy, pool, _weaponUpgradeSystem, config, id);
                case "Shotgun":
                    var shotgunConfig = await _weaponConfigLoader.LoadAsync<ShotgunConfig>(id);
                    return new ShotgunAdapter(hierarchy, pool, _weaponUpgradeSystem, shotgunConfig, id);
            }
        }
    }
}