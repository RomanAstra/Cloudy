using Cysharp.Threading.Tasks;
using Extensions;
using UnityEngine;

namespace Cloudy
{
    public class LevelConfigLoader : LocalAssetLoader
    {
        public async UniTask<LevelConfig> LoadAsync(string levelName)
        {
            return (LevelConfig)await LoadScriptableObjectAsync(levelName);
        }
    }
}