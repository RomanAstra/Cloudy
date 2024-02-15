using UnityEngine;

namespace Cloudy.Configs
{
    [CreateAssetMenu(menuName = "Configs/Weapon/Shotgun", fileName = nameof(ShotgunConfig))]
    public sealed class ShotgunConfig : WeaponConfig
    {
        public int BulletsCount;
    }
}