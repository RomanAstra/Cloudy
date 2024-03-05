using System.Collections.Generic;
using UnityEngine;

namespace Cloudy
{
    //[CreateAssetMenu(menuName = "WeaponsConfig", fileName = nameof(WeaponsConfig))]
    public sealed class OpenObjectStarsConfig : ScriptableObject
    {
        [SerializeField] private OpenObjectStarsData[] _objects;
        
        public IReadOnlyList<OpenObjectStarsData> Objects => _objects;
    }
}