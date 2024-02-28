using Cysharp.Threading.Tasks;
using Extensions;
using UnityEngine;

namespace Cloudy
{
    public sealed class LevelProvider : LocalAssetLoader
    {
        public async UniTask<LevelView> Load(string levelName)
        {
            return await InstantiateAsync<LevelView>(levelName);
        }
    }
}