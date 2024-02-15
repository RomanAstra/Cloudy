using System;
using Cloudy.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;

namespace Cloudy.Pools
{
    public sealed class CloudPoolFactory
    {
        private readonly GameManager _gameManager;
        private readonly CloudSpawner _cloudSpawner = new();
        private readonly CloudConfigLoader _cloudConfigLoader = new ();

        public CloudPoolFactory(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        public async UniTask<IConcreteCloudPool> GetCloudPool(string id, Transform parent)
        {
            IConcreteCloudPool pool;
            
            switch (id)
            {
                default:
                    var hierarchy = await _cloudSpawner.LoadAsync<CloudHierarchy>(id);
                    var config = await _cloudConfigLoader.LoadAsync<CloudConfig>(id);
                    pool = new CloudPool(parent, hierarchy, _gameManager, config);
                    break;
                case "BeatOff":
                    var beatOffCloudHierarchy = await _cloudSpawner.LoadAsync<BeatOffCloudHierarchy>(id);
                    var beatOffConfig = await _cloudConfigLoader.LoadAsync<BeatOffCloudConfig>(id);
                    pool = new BeatOffCloudPool(parent, beatOffCloudHierarchy, _gameManager, beatOffConfig);
                    break;
                case "Immortal":
                    var immortalHierarchy = await _cloudSpawner.LoadAsync<ImmortalCloudHierarchy>(id);
                    var immortalConfig = await _cloudConfigLoader.LoadAsync<ImmortalCloudConfig>(id);
                    pool = new ImmortalCloudPool(parent, immortalHierarchy, _gameManager, immortalConfig);
                    break;
                case "Moving":
                    var movingHierarchy = await _cloudSpawner.LoadAsync<MovingCloudHierarchy>(id);
                    var movingConfig = await _cloudConfigLoader.LoadAsync<MovingCloudConfig>(id);
                    pool = new MovingCloudPool(parent, movingHierarchy, _gameManager, movingConfig);
                    break;
                case "Regenerating":
                    var regeneratingHierarchy = await _cloudSpawner.LoadAsync<CloudHierarchy>(id);
                    var regeneratingConfig = await _cloudConfigLoader.LoadAsync<RegeneratingCloudConfig>(id);
                    pool = new RegeneratingCloudPool(parent, regeneratingHierarchy, _gameManager, regeneratingConfig);
                    break;
            }

            return pool;
        }
    }
}