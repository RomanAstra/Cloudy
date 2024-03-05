using System;
using System.Collections.Generic;
using Cloudy.Pools;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Cloudy
{
    public sealed class LevelModel : ILevelModel
    {
        private readonly CloudPoolFactory _cloudPoolFactory;
        private readonly GameManager _gameManager;
        private readonly LevelConfigLoader _levelConfigLoader = new();
        private  CloudSpawnZoneAdapter[] _cloudSpawnZoneAdapters;
        private IReadOnlyList<CloudSpawnZoneHierarchy> _cloudSpawnZones;
        
        public string LevelName { get; private set; }

        public LevelModel(CloudPoolFactory cloudPoolFactory, GameManager gameManager)
        {
            _cloudPoolFactory = cloudPoolFactory;
            _gameManager = gameManager;
        }

        public async UniTask CreateSpawnZones(IReadOnlyList<CloudSpawnZoneHierarchy> cloudSpawnZones, string levelName)
        {
            _cloudSpawnZones = cloudSpawnZones;
            LevelName = levelName;
            _cloudSpawnZoneAdapters = new CloudSpawnZoneAdapter[_cloudSpawnZones.Count];
            var config = await _levelConfigLoader.LoadAsync(levelName.Replace("(Clone)", ""));

            for (var i = 0; i < _cloudSpawnZoneAdapters.Length; i++)
            {
                var zone = _cloudSpawnZones[i];
                var pool = await _cloudPoolFactory.GetCloudPool(zone.CloudName, zone.transform);
                var settings = config.ZonesSettings[i];
                var adapter = new CloudSpawnZoneAdapter(zone, pool, settings);
                _cloudSpawnZoneAdapters[i] = adapter;

                _gameManager.Add(adapter);
            }
        }
        public void OnDestroy()
        {
            foreach (var adapter in _cloudSpawnZoneAdapters)
            {
                _gameManager.Remove(adapter);
            }
        }
    }
}