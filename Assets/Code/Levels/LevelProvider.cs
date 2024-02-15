using Cysharp.Threading.Tasks;
using Extensions;
using UnityEngine;

namespace Cloudy
{
    public sealed class LevelProvider : LocalAssetLoader
    {
        public async UniTask<LevelHierarchy> Load(string levelName)
        {
            return await InstantiateAsync<LevelHierarchy>($"Location{App.CurrentLocation}Level{levelName}");
        }
    }
}