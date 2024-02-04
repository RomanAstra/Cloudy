using Cysharp.Threading.Tasks;
using Extensions;
using UnityEngine;

namespace Cloudy
{
    public sealed class LevelProvider : LocalAssetLoader
    {
        public async UniTask<Transform> Load(string levelName)
        {
            return await InstantiateAsync<Transform>($"Location{App.CurrentLocation}Level{levelName}");
        }
    }
}