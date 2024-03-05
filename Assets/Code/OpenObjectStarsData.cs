using System;
using UnityEngine;

namespace Cloudy
{
    [Serializable]
    public sealed class OpenObjectStarsData
    {
        [field: SerializeField]
        public string Id { get; private set; }
        [field: SerializeField]
        public int StarsCount { get; private set; }
    }
}