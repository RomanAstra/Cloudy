using Cloudy.Adapter;
using Cloudy.Configs;
using UnityEngine;
using Utils;

namespace Cloudy.Pools
{
    public sealed class BeatOffCloudPool : IConcreteCloudPool
    {
        private readonly Transform _parent;
        private readonly GameManager _gameManager;
        private readonly BeatOffCloudConfig _config;
        private readonly Pool<BeatOffCloudHierarchy> _hierarchyPool;

        public BeatOffCloudPool(Transform parent, BeatOffCloudHierarchy cloudPrefab, 
            GameManager gameManager, BeatOffCloudConfig config)
        {
            _parent = parent;
            _gameManager = gameManager;
            _config = config;
            _hierarchyPool = new Pool<BeatOffCloudHierarchy>(cloudPrefab);
        }
        
        public void Spawn(Vector3 position)
        {
            var hierarchy = _hierarchyPool.Get(position, Quaternion.identity, _parent);
            var adapter = new BeatOffCloudAdapter(hierarchy, _config);
            adapter.OnRelease += OnRelease;
            _gameManager.Add(adapter);
        }

        private void OnRelease(CloudAdapter adapter, CloudHierarchy hierarchy)
        {
            _hierarchyPool.Release((BeatOffCloudHierarchy)hierarchy);
            _gameManager.Remove(adapter);
            adapter.OnRelease -= OnRelease;
        }
    }
}