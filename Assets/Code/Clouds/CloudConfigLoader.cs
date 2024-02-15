using Cloudy.Configs;
using Cysharp.Threading.Tasks;
using Extensions;

namespace Cloudy
{
    public sealed class CloudConfigLoader : LocalAssetLoader
    {
        public async UniTask<T> LoadAsync<T>(string weaponName) where T : CloudConfig
        {
            return (T)await LoadScriptableObjectAsync($"{weaponName}CloudConfig");
        }
    }
}