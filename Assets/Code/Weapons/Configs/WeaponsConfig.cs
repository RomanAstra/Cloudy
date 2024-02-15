using UnityEngine;

namespace Cloudy
{
    [CreateAssetMenu(menuName = "WeaponsConfig", fileName = nameof(WeaponsConfig))]
    public sealed class WeaponsConfig : ScriptableObject
    {
        public string[] Weapons;
    }
}