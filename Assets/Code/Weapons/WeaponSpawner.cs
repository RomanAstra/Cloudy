using Cysharp.Threading.Tasks;
using Extensions;
using UnityEngine;

namespace Cloudy
{
    public sealed class WeaponSpawner : LocalAssetLoader
    {
        public async UniTask<WeaponHierarchy> InstantiateAsync(string weaponName, Transform parent)
        {
            var weapon = await InstantiateAsync<WeaponHierarchy>(weaponName, parent);
            weapon.gameObject.SetActive(false);

            return weapon;
        }
    }
}