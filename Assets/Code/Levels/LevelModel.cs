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
        private string _levelName;

        public LevelModel(CloudPoolFactory cloudPoolFactory, GameManager gameManager)
        {
            _cloudPoolFactory = cloudPoolFactory;
            _gameManager = gameManager;
        }

        public async UniTask CreateSpawnZones(IReadOnlyList<CloudSpawnZoneHierarchy> cloudSpawnZones, string levelName)
        {
            _cloudSpawnZones = cloudSpawnZones;
            _levelName = levelName;
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
        public void CreateSpawnZonesConfig()
        {
            if (_cloudSpawnZones.Count == 0)
                return;

            var levelConfig = ScriptableObject.CreateInstance<LevelConfig>();
            levelConfig.ZonesSettings = new ZoneSettings[_cloudSpawnZones.Count];
            for (var i = 0; i < _cloudSpawnZones.Count; i++)
            {
                var zone = _cloudSpawnZones[i];
                var objZone = new ZoneSettings();
                levelConfig.ZonesSettings[i] = objZone;

                objZone.CloudName = zone.CloudName;
                objZone.SizeZone = zone.SizeZone;
                objZone.SpawnDelay = zone.SpawnDelay;
                objZone.StartSpawnDelay = zone.StartSpawnDelay;
            }

            var levelName = _levelName;
            var indexSubstring = levelName.IndexOf("Level", StringComparison.Ordinal);
            var location = levelName.Substring(0, indexSubstring);
            var level = levelName.Substring(indexSubstring);

            var folder = $"Assets/Configs/Levels/{location}";
            if (!AssetDatabase.IsValidFolder(folder))
            {
                var guid = AssetDatabase.CreateFolder("Assets/Configs/Levels", location);
                AssetDatabase.GUIDToAssetPath(guid);
            }

            AssetDatabase.CreateAsset(levelConfig, $"{folder}/{location}{level}.asset");
        }
    }
}