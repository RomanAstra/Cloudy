using Cloudy.Adapter;
using Cloudy.Configs;
using UnityEngine;
using Utils;

namespace Cloudy.Pools
{
    public sealed class CloudPool : IConcreteCloudPool
    {
        private readonly Transform _parent;
        private readonly GameManager _gameManager;
        private readonly CloudConfig _config;
        private readonly Pool<CloudHierarchy> _hierarchyPool;

        public CloudPool(Transform parent, CloudHierarchy cloudPrefab, 
            GameManager gameManager, CloudConfig config)
        {
            _parent = parent;
            _gameManager = gameManager;
            _config = config;
            _hierarchyPool = new Pool<CloudHierarchy>(cloudPrefab);
        }
        
        public void Spawn(Vector3 position)
        {
            var hierarchy = _hierarchyPool.Get(position, Quaternion.identity, _parent);
            var adapter = new CloudAdapter(hierarchy, _config);
            adapter.OnRelease += OnRelease;
            _gameManager.Add(adapter);
        }

        private void OnRelease(CloudAdapter adapter, CloudHierarchy hierarchy)
        {
            _hierarchyPool.Release(hierarchy);
            _gameManager.Remove(adapter);
            adapter.OnRelease -= OnRelease;
        }
    }
}