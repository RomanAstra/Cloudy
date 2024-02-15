﻿using Cloudy.Adapter;
using Cloudy.Configs;
using UnityEngine;
using Utils;

namespace Cloudy.Pools
{
    public sealed class MovingCloudPool : IConcreteCloudPool
    {
        private readonly Transform _parent;
        private readonly GameManager _gameManager;
        private readonly MovingCloudConfig _config;
        private readonly Pool<MovingCloudHierarchy> _hierarchyPool;

        public MovingCloudPool(Transform parent, MovingCloudHierarchy cloudPrefab, 
            GameManager gameManager, MovingCloudConfig config)
        {
            _parent = parent;
            _gameManager = gameManager;
            _config = config;
            _hierarchyPool = new Pool<MovingCloudHierarchy>(cloudPrefab);
        }
        
        public void Spawn(Vector3 position)
        {
            var hierarchy = _hierarchyPool.Get(position, Quaternion.identity, _parent);
            var adapter = new MovingCloudAdapter(hierarchy, _config);
            adapter.OnRelease += OnRelease;
            _gameManager.Add(adapter);
        }
        
        private void OnRelease(CloudAdapter adapter, CloudHierarchy hierarchy)
        {
            _hierarchyPool.Release((MovingCloudHierarchy)hierarchy);
            _gameManager.Remove(adapter);
            adapter.OnRelease -= OnRelease;
        }
    }
}