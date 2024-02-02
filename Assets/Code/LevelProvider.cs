using Cysharp.Threading.Tasks;
using Extensions;
using UnityEngine;

namespace Cloudy
{
    public sealed class LevelProvider : LocalAssetLoader
    {
        public async UniTask<Transform> Load(string levelName)
        {
            Debug.Log($"Location{App.CurrentLocation}Level{levelName}");
            return await Load<Transform>($"Location{App.CurrentLocation}Level{levelName}");
        }
    }
}