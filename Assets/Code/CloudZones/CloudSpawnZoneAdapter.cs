using Cloudy.Pools;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;

namespace Cloudy
{
    public sealed class CloudSpawnZoneAdapter : IUpdate
    {
        private readonly IConcreteCloudPool _pool;
        private readonly Countdown _spawnDelay;
        private readonly Transform _transform;
        private readonly float _width;
        private readonly float _height;
        
        public CloudSpawnZoneAdapter(CloudSpawnZoneHierarchy hierarchy, IConcreteCloudPool pool, ZoneSettings settings)
        {
            _transform = hierarchy.transform;
            _pool = pool;

            _spawnDelay = new Countdown(settings.SpawnDelay, settings.StartSpawnDelay);
            
            _width = settings.SizeZone.x * 0.5f;
            _height = settings.SizeZone.y * 0.5f;
        }
        
        public void OnUpdate(float deltaTime)
        {
            _spawnDelay.Update();
            
            if(!_spawnDelay.IsEnded)
                return;
            
            _pool.Spawn(GetRandomPosition());
            
            _spawnDelay.Reset();
        }
        
        
        private Vector3 GetRandomPosition()
        {
            return _transform.position + new Vector3(Random.Range(-_width, _width), Random.Range(-_height, _height));
        }
    }
}