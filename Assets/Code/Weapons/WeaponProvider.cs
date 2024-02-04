using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Extensions;
using UnityEngine;

namespace Cloudy
{
    public sealed class WeaponsProvider : LocalAssetLoader
    {
        public async UniTask<Weapon[]> InstantiateAsync(List<string> weaponNames, Transform parent)
        {
            var weapons = new Weapon[weaponNames.Count];

            for (var i = 0; i < weaponNames.Count; i++)
            {
                var weapon = await InstantiateAsync<Weapon>(weaponNames[i], parent);
                weapon.gameObject.SetActive(false);
                weapons[i] = weapon;
            }

            return weapons;
        }
    }
}