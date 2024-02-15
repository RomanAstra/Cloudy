using System;
using Cloudy.Configs;
using Configs.Upgrades;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;

namespace Cloudy
{
    public sealed class BulletPoolFactory
    {
        private readonly Transform _bulletParent;
        private readonly WeaponUpgradeSystem _weaponUpgradeSystem;
        private readonly GameManager _gameManager;
        private readonly BulletLoader _bulletLoader = new ();
        private readonly BulletConfigLoader _bulletConfigLoader = new ();

        public BulletPoolFactory(Transform bulletParent, WeaponUpgradeSystem weaponUpgradeSystem, GameManager gameManager)
        {
            _bulletParent = bulletParent;
            _weaponUpgradeSystem = weaponUpgradeSystem;
            _gameManager = gameManager;
        }

        public async UniTask<IConcreteBulletPool> GetBulletPool(string id)
        {
            IConcreteBulletPool pool;
            var hierarchy = await _bulletLoader.LoadAsync(id);
            switch (id)
            {
                default:
                    
                    var config = await _bulletConfigLoader.LoadAsync<BulletConfig>(id);
                    pool = new BulletPool(_bulletParent, hierarchy, _weaponUpgradeSystem, _gameManager, config, id);
                    break; 
                case "Freezing":
                    //var freezingHierarchy = await _bulletLoader.LoadAsync<FreezingBulletHierarchy>(id);
                    var freezingConfig = await _bulletConfigLoader.LoadAsync<FreezingBulletConfig>(id);
                    pool = new FreezingBulletPool(_bulletParent, hierarchy, _weaponUpgradeSystem, _gameManager, freezingConfig, id);
                    break;
                case "Strengthening":
                    //var strengtheningHierarchy = await _bulletLoader.LoadAsync<StrengtheningBulletHierarchy>(id);
                    var strengtheningConfig = await _bulletConfigLoader.LoadAsync<StrengtheningBulletConfig>(id);
                    pool = new StrengtheningBulletPool(_bulletParent, hierarchy, _weaponUpgradeSystem, _gameManager, strengtheningConfig, id);
                    break;
                
            }
            
            return pool;
        }
    }
}