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
        private readonly AudioSource _audioSource;
        private readonly ParticleSystem _particleSystem;
        private readonly Pool<BeatOffCloudHierarchy> _hierarchyPool;

        public BeatOffCloudPool(Transform parent, BeatOffCloudHierarchy cloudPrefab, GameManager gameManager,
            BeatOffCloudConfig config, AudioSource audioSource, ParticleSystem particleSystem)
        {
            _parent = parent;
            _gameManager = gameManager;
            _config = config;
            _audioSource = audioSource;
            _particleSystem = particleSystem;
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
            
            _particleSystem.transform.position = hierarchy.transform.position;
            _particleSystem.Play();
            _audioSource.PlayOneShot(hierarchy.ExplosionAudioClip);
        }
    }
}