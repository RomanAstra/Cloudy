using System.Collections.Generic;
using Cloudy.Adapter;
using Cloudy.Configs;
using Code;
using UnityEngine;
using Utils;

namespace Cloudy.Pools
{
    public sealed class BeatOffCloudPool : IConcreteCloudPool
    {
        private readonly Transform _parent;
        private readonly GameManager _gameManager;
        private readonly BeatOffCloudConfig _config;
        private readonly AudioSource _audioSource;
        private readonly ParticlePool _particlePool;
        private readonly Pool<BeatOffCloudHierarchy> _hierarchyPool;
        private readonly List<CloudHierarchy> _clouds = new();
        
        public IReadOnlyList<CloudHierarchy> Clouds => _clouds;

        public BeatOffCloudPool(Transform parent, BeatOffCloudHierarchy cloudPrefab, GameManager gameManager,
            BeatOffCloudConfig config, AudioSource audioSource, ParticlePool particlePool)
        {
            _parent = parent;
            _gameManager = gameManager;
            _config = config;
            _audioSource = audioSource;
            _particlePool = particlePool;
            _hierarchyPool = new Pool<BeatOffCloudHierarchy>(cloudPrefab);
        }
        
        public void Spawn(Vector3 position)
        {
            var hierarchy = _hierarchyPool.Get(position, Quaternion.identity, _parent);
            var adapter = new BeatOffCloudAdapter(hierarchy, _config);
            adapter.OnRelease += OnRelease;
            _gameManager.Add(adapter);
            _clouds.Add(hierarchy);
        }

        private void OnRelease(CloudAdapter adapter, CloudHierarchy hierarchy)
        {
            _hierarchyPool.Release((BeatOffCloudHierarchy)hierarchy);
            _gameManager.Remove(adapter);
            _clouds.Remove(hierarchy);
            adapter.OnRelease -= OnRelease;
            
            _particlePool.Spawn(hierarchy.transform.position, _config.ExplosionRatio).Forget();
            _audioSource.PlayOneShot(hierarchy.ExplosionAudioClip);
        }
    }
}