using System;
using UnityEngine;

namespace Cloudy
{
    [Serializable]
    public sealed class ZoneSettings
    {
        public string CloudName;
        public Vector2 SizeZone;
        public float SpawnDelay;
        public float StartSpawnDelay;
    }
}