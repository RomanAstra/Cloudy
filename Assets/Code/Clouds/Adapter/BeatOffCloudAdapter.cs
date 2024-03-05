using Cloudy.Configs;
using UnityEngine;

namespace Cloudy.Adapter
{
    public sealed class BeatOffCloudAdapter : CloudAdapter
    {
        private readonly BeatOffCloudHierarchy _hierarchy;
        private readonly Countdown _shieldDelay = new ();
        
        public BeatOffCloudAdapter(BeatOffCloudHierarchy hierarchy, BeatOffCloudConfig config) : base(hierarchy, config)
        {
            _hierarchy = hierarchy;
            //_hierarchy.ShieldComponent.onCollisionEnter += OnCollisionEnter;

            _shieldDelay.Duration = config.ShieldDelay;
        }
        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            
            if(_hierarchy.ShieldComponent.gameObject.activeSelf || _currentHitPoints == 0)
                return;
            
            _shieldDelay.Update();
            
            if(_shieldDelay.IsEnded)
                ShowShield();
        }

        private void OnCollisionEnter()
        {
            _hierarchy.ShieldComponent.gameObject.SetActive(false);
            _hierarchy.HitCollider.enabled = true;
            _shieldDelay.Reset();
        }
        private void ShowShield()
        {
            _hierarchy.ShieldComponent.gameObject.SetActive(true);
            _hierarchy.HitCollider.enabled = false;
        }
        protected override void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent<BulletHierarchy>(out var bullet))
            {
                _hierarchy.ShieldComponent.gameObject.SetActive(false);
                _hierarchy.HitCollider.enabled = true;
                _shieldDelay.Reset();
                return;
            }
            
            base.OnCollisionEnter(collision);
        }
        // protected override void Release()
        // {
        //     base.Release();
        //     
        //     _hierarchy.ShieldComponent.onCollisionEnter -= OnCollisionEnter;
        // }
    }
}