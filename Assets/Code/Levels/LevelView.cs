using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
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
            _levelModel.CreateSpawnZonesConfig();
        }
    }
}