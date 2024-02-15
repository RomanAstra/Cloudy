using Cloudy.Configs;
using UnityEngine;
using Utils;

namespace Cloudy
{
    public sealed class FreezingBulletPool : IConcreteBulletPool
    {
        private readonly Transform _parent;
        private readonly Pool<BulletHierarchy> _hierarchyPool;
        private readonly WeaponUpgradeSystem _weaponUpgradeSystem;
        private readonly GameManager _gameManager;
        private readonly FreezingBulletConfig _config;
        private readonly string _id;

        public FreezingBulletPool(Transform parent, BulletHierarchy bulletPrefab, WeaponUpgradeSystem weaponUpgradeSystem,
            GameManager gameManager, FreezingBulletConfig config, string id)
        {
            _hierarchyPool = new Pool<BulletHierarchy>(bulletPrefab);
            _parent = parent;
            _weaponUpgradeSystem = weaponUpgradeSystem;
            _gameManager = gameManager;
            _config = config;
            _id = id;
        }
        
        public void Spawn(Vector3 position, Quaternion rotation, Vector3 direction)
        {
            var hierarchy = _hierarchyPool.Get(position, rotation, _parent);
            var adapter = new FreezingBulletAdapter(hierarchy, _weaponUpgradeSystem, _config, _id);
            adapter.OnRelease += OnRelease;
            adapter.Move(direction);
            _gameManager.Add(adapter);
        }
        
        private void OnRelease(BulletAdapter adapter, BulletHierarchy hierarchy)
        {
            _hierarchyPool.Release(hierarchy);
            _gameManager.Remove(adapter);
            adapter.OnRelease -= OnRelease;
        }
    }
}