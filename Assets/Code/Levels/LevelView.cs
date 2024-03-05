using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Cloudy
{
    public sealed class LevelView : MonoBehaviour
    {
        [SerializeField] private CloudSpawnZoneHierarchy[] _cloudSpawnZones;
        private ILevelModel _levelModel;

        public void Initialize(ILevelModel levelModel)
        {
            _levelModel = levelModel;
            _levelModel.CreateSpawnZones(_cloudSpawnZones, gameObject.name);
        }

        private void OnDestroy()
        {
            _levelModel.OnDestroy();
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