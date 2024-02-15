using Cloudy.Adapter;
using Cloudy.Configs;
using UnityEngine;
using Utils;

namespace Cloudy.Pools
{
    public sealed class RegeneratingCloudPool : IConcreteCloudPool
    {
        private readonly Transform _parent;
        private readonly GameManager _gameManager;
        private readonly RegeneratingCloudConfig _config;
        private readonly Pool<CloudHierarchy> _hierarchyPool;

        public RegeneratingCloudPool(Transform parent, CloudHierarchy cloudPrefab, 
            GameManager gameManager, RegeneratingCloudConfig config)
        {
            _parent = parent;
            _gameManager = gameManager;
            _config = config;
            _hierarchyPool = new Pool<CloudHierarchy>(cloudPrefab);
        }
        
        public void Spawn(Vector3 position)
        {
            var hierarchy = _hierarchyPool.Get(position, Quaternion.identity, _parent);
            var adapter = new RegeneratingCloudAdapter(hierarchy, _config);
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