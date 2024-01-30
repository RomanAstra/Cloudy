using JetBrains.Annotations;
using UnityEngine;

namespace Cloudy
{
    [UsedImplicitly]
    public sealed class PistolBulletSpawner : BulletSpawner
    {
        private PistolBulletSpawner(PistolBulletPool pool, Transform parent) : base(pool, parent)
        {
        }
    }
}