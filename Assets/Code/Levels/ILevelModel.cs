using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Cloudy
{
    public interface ILevelModel
    {
        string LevelName { get; }
        
        UniTask CreateSpawnZones(IReadOnlyList<CloudSpawnZoneHierarchy> cloudSpawnZones, string levelName);
        void OnDestroy();
    }
}