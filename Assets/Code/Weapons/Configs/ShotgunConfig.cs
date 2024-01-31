using UnityEngine;

namespace Cloudy.Configs
{
    [CreateAssetMenu(fileName = nameof(ShotgunConfig), menuName = "Configs/Weapon/Shotgun")]
    public sealed class ShotgunConfig : WeaponConfig
    {
        public int bulletsCount;
        public float inaccuracy;
    }
}