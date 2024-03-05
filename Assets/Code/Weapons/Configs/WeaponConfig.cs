using UnityEngine;

namespace Cloudy.Configs
{
    [CreateAssetMenu(menuName = "Configs/Weapon/Weapon", fileName = nameof(WeaponConfig))]
    public class WeaponConfig : ScriptableObject
    {
        public float FireDelay;
        public float Inaccuracy;
        public float RecoilPower = 10f;
        public float RotationSpeed = 30f;
    }
}