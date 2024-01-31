using System;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Cloudy
{
    public class CloudSpawnZone : MonoBehaviour
    {
        [SerializeField] private Cloud _cloud;
        [SerializeField] private Vector2 _sizeZone;
        [SerializeField] private Countdown _spawnDelay;

        private Pool<Cloud> _pool;
        private Transform _transform;
        private float _width;
        private float _height;
        
        private void Awake()
        {
            _pool = new Pool<Cloud>(_cloud);
            _transform = transform;
            
            _width = _sizeZone.x * 0.5f;
            _height = _sizeZone.y * 0.5f;
        }
        private void Update()
        {
            _spawnDelay.Update();
            
            if(!_spawnDelay.IsEnded)
                return;
            
            Spawn();
            _spawnDelay.Reset();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(_sizeZone.x, _sizeZone.y, 0.1f));
        }

        private void Spawn()
        {
            var cloud = _pool.Get(GetRandomPosition(), Quaternion.identity, _transform);
            cloud.SetPool(_pool);
        }
        private Vector3 GetRandomPosition()
        {
            return _transform.position + new Vector3(Random.Range(-_width, _width), Random.Range(-_height, _height));
        }
    }
}