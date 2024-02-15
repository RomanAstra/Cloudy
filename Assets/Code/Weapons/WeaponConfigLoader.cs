using Cloudy.Configs;
using Cysharp.Threading.Tasks;
using Extensions;
using UnityEngine;

namespace Cloudy
{
    public sealed class WeaponConfigLoader : LocalAssetLoader
    {
        public async UniTask<T> LoadAsync<T>(string weaponName) where T : ScriptableObject
        {
            return (T)await LoadScriptableObjectAsync($"{weaponName}Config");
        }
    }
}