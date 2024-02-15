using System;
using System.Collections.Generic;
using Cloudy.Pools;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Utils;
using Zenject;

namespace Cloudy
{
    public sealed class LevelHierarchy : MonoBehaviour
    {
        [SerializeField] private CloudSpawnZoneHierarchy[] _cloudSpawnZones;

        private CloudSpawnZoneAdapter[] _cloudSpawnZoneAdapters;

        private GameManager _gameManager;
        private readonly LevelConfigLoader _levelConfigLoader = new();

        [Inject]
        public async UniTaskVoid Construct(CloudPoolFactory cloudPoolFactory, GameManager gameManager, DiContainer container)
        {
            _gameManager = gameManager;
            _cloudSpawnZoneAdapters = new CloudSpawnZoneAdapter[_cloudSpawnZones.Length];
            var config = await _levelConfigLoader.LoadAsync(gameObject.name.Replace("(Clone)", ""));

            for (var i = 0; i < _cloudSpawnZoneAdapters.Length; i++)
            {
                var zone = _cloudSpawnZones[i];
                var pool = await cloudPoolFactory.GetCloudPool(zone.CloudName, zone.transform);
                var settings = config.ZonesSettings[i];
                var adapter = new CloudSpawnZoneAdapter(zone, pool, settings);
                _cloudSpawnZoneAdapters[i] = adapter;

                _gameManager.Add(adapter);
            }
        }

        private void OnDestroy()
        {
            foreach (var adapter in _cloudSpawnZoneAdapters)
            {
                _gameManager.Remove(adapter);
            }
        }

        [Button]
        private void GetCloudSpawnZones()
        {
            _cloudSpawnZones = GetComponentsInChildren<CloudSpawnZoneHierarchy>();
        }
        [Button]
        private void CreateSpawnZonesConfig()
        {
            if (_cloudSpawnZones.Length == 0)
                return;

            var levelConfig = ScriptableObject.CreateInstance<LevelConfig>();
            levelConfig.ZonesSettings = new ZoneSettings[_cloudSpawnZones.Length];
            for (var i = 0; i < _cloudSpawnZones.Length; i++)
            {
                var zone = _cloudSpawnZones[i];
                var objZone = new ZoneSettings();
                levelConfig.ZonesSettings[i] = objZone;

                objZone.CloudName = zone.CloudName;
                objZone.SizeZone = zone.SizeZone;
                objZone.SpawnDelay = zone.SpawnDelay;
                objZone.StartSpawnDelay = zone.StartSpawnDelay;
            }

            var levelName = gameObject.name;
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