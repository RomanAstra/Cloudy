using Cloudy.Configs;
using Configs.Upgrades;
using UnityEngine;

namespace Cloudy
{
    public sealed class FreezingComponent : MonoBehaviour
    {
        [SerializeField] private float _freezingTime = 3f;

        private void Awake()
        {
            _freezingTime += WeaponUpgradeSystem.GetValue(WeaponType.Freezing, StatType.FreezingTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            var cloud = other.GetComponentInParent<Cloud>();
            if (cloud == null)
                return;

            cloud.SetFrozen(_freezingTime);
        }
    }
}