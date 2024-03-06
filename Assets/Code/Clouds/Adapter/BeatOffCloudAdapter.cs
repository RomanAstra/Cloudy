using Cloudy.Configs;
using UnityEngine;

namespace Cloudy.Adapter
{
    public sealed class BeatOffCloudAdapter : CloudAdapter
    {
        private readonly BeatOffCloudHierarchy _hierarchy;
        
        public BeatOffCloudAdapter(BeatOffCloudHierarchy hierarchy, BeatOffCloudConfig config) : base(hierarchy, config)
        {
            _hierarchy = hierarchy;

            SetActiveShield(true);
        }

        protected override void OnDealDamage(int damage)
        {
            base.OnDealDamage(damage);
            SetActiveShield(true);
        }
        protected override void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent<BulletHierarchy>(out var bullet))
            {
                SetActiveShield(false);
                return;
            }
            
            base.OnCollisionEnter(collision);
        }
        private void SetActiveShield(bool isActive)
        {
            _hierarchy.ShieldComponent.gameObject.SetActive(isActive);
            _hierarchy.HitCollider.enabled = !isActive;
        }
    }
}