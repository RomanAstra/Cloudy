using Cysharp.Threading.Tasks;
using Extensions;

namespace Cloudy
{
    public sealed class BulletLoader : LocalAssetLoader
    {
        public async UniTask<BulletHierarchy> LoadAsync(string weaponName)
        {
            return await LoadGameObjectAsync<BulletHierarchy>($"{weaponName}Bullet");
        }
    }
}