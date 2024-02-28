using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Cloudy
{
    public interface ILevelModel
    {
        UniTask CreateSpawnZones(IReadOnlyList<CloudSpawnZoneHierarchy> cloudSpawnZones, string levelName);
        void OnDestroy();
        void CreateSpawnZonesConfig();
    }
}