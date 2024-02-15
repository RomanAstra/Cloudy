using Cloudy.Configs;
using Cysharp.Threading.Tasks;
using Extensions;

namespace Cloudy
{
    public sealed class BulletConfigLoader : LocalAssetLoader
    {
        public async UniTask<T> LoadAsync<T>(string weaponName) where T : BulletConfig
        {
            return (T)await LoadScriptableObjectAsync($"{weaponName}BulletConfig");
        }
    }
}