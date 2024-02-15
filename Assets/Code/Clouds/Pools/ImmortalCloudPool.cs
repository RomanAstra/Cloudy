using Cloudy.Adapter;
using Cloudy.Configs;
using UnityEngine;
using Utils;

namespace Cloudy.Pools
{
    public sealed class ImmortalCloudPool : IConcreteCloudPool
    {
        private readonly Transform _parent;
        private readonly GameManager _gameManager;
        private readonly ImmortalCloudConfig _config;
        private readonly Pool<ImmortalCloudHierarchy> _hierarchyPool;

        public ImmortalCloudPool(Transform parent, ImmortalCloudHierarchy cloudPrefab, 
            GameManager gameManager, ImmortalCloudConfig config)
        {
            _parent = parent;
            _gameManager = gameManager;
            _config = config;
            _hierarchyPool = new Pool<ImmortalCloudHierarchy>(cloudPrefab);
        }
        
        public void Spawn(Vector3 position)
        {
            var hierarchy = _hierarchyPool.Get(position, Quaternion.identity, _parent);
            var adapter = new ImmortalCloudAdapter(hierarchy, _config);
            adapter.OnRelease += OnRelease;
            _gameManager.Add(adapter);
        }

        private void OnRelease(CloudAdapter adapter, CloudHierarchy hierarchy)
        {
            _hierarchyPool.Release((ImmortalCloudHierarchy)hierarchy);
            _gameManager.Remove(adapter);
            adapter.OnRelease -= OnRelease;
        }
    }
}