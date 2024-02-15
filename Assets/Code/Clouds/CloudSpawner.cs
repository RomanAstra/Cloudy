using Cysharp.Threading.Tasks;
using Extensions;

namespace Cloudy
{
    public sealed class CloudSpawner : LocalAssetLoader
    {
        public async UniTask<T> LoadAsync<T>(string name) where T : CloudHierarchy
        {
            return await LoadGameObjectAsync<T>(name);
        }
    }
}