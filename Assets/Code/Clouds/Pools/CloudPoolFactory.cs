using System;
using System.Collections.Generic;
using Cloudy.Configs;
using Code;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;

namespace Cloudy.Pools
{
    public sealed class CloudPoolFactory
    {
        private readonly GameManager _gameManager;
        private readonly AudioSource _audioSource;
        private readonly ParticlePool _particlePool;
        private readonly CloudSpawner _cloudSpawner = new();
        private readonly CloudConfigLoader _cloudConfigLoader = new ();
        private readonly Dictionary<string, IConcreteCloudPool> _pools = new ();
        
        public IReadOnlyCollection<IConcreteCloudPool> Pools => _pools.Values;

        public CloudPoolFactory(GameManager gameManager, AudioSource audioSource, ParticlePool particlePool)
        {
            _gameManager = gameManager;
            _audioSource = audioSource;
            _particlePool = particlePool;
        }
        
        public async UniTask<IConcreteCloudPool> GetCloudPool(string id, string type, Transform parent)
        {
            if(_pools.TryGetValue(id, out var pool))
               return pool;
            
            switch (type)
            {
                default:
                    var hierarchy = await _cloudSpawner.LoadAsync<CloudHierarchy>(id);
                    var config = await _cloudConfigLoader.LoadAsync<CloudConfig>(id);
                    pool = new CloudPool(parent, hierarchy, _gameManager, config, _audioSource, _particlePool);
                    break;
                case "BeatOff":
                    var beatOffCloudHierarchy = await _cloudSpawner.LoadAsync<BeatOffCloudHierarchy>(id);
                    var beatOffConfig = await _cloudConfigLoader.LoadAsync<BeatOffCloudConfig>(id);
                    pool = new BeatOffCloudPool(
                        parent, beatOffCloudHierarchy, _gameManager, beatOffConfig, _audioSource, _particlePool);
                    break;
                case "Immortal":
                    var immortalHierarchy = await _cloudSpawner.LoadAsync<ImmortalCloudHierarchy>(id);
                    var immortalConfig = await _cloudConfigLoader.LoadAsync<ImmortalCloudConfig>(id);
                    pool = new ImmortalCloudPool(
                        parent, immortalHierarchy, _gameManager, immortalConfig, _audioSource, _particlePool);
                    break;
                case "Moving":
                    var movingHierarchy = await _cloudSpawner.LoadAsync<MovingCloudHierarchy>(id);
                    var movingConfig = await _cloudConfigLoader.LoadAsync<MovingCloudConfig>(id);
                    pool = new MovingCloudPool
                        (parent, movingHierarchy, _gameManager, movingConfig, _audioSource, _particlePool);
                    break;
                case "Regenerating":
                    var regeneratingHierarchy = await _cloudSpawner.LoadAsync<CloudHierarchy>(id);
                    var regeneratingConfig = await _cloudConfigLoader.LoadAsync<RegeneratingCloudConfig>(id);
                    pool = new RegeneratingCloudPool(
                        parent, regeneratingHierarchy, _gameManager, regeneratingConfig, _audioSource, _particlePool);
                    break;
            }

            _pools.Add(id, pool);
            return pool;
        }
    }
}