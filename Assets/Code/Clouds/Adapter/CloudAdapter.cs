using System;
using Cloudy.Configs;
using UnityEngine;
using Utils;

namespace Cloudy.Adapter
{
    public class CloudAdapter : IUpdate
    {
        private readonly CloudHierarchy _hierarchy;
        private readonly float _captureSpeed;
        protected int _currentHitPoints;
        private readonly Countdown _frozenTime = new ();
        
        public event Action<CloudAdapter, CloudHierarchy> OnRelease;

        public CloudAdapter(CloudHierarchy hierarchy, CloudConfig config)
        {
            _hierarchy = hierarchy;

            _hierarchy.OnDealDamage += OnDealDamage;
            _hierarchy.OnSetFrozen += OnSetFrozen;
            _hierarchy.CollisionEnter += OnCollisionEnter;

            _captureSpeed = config.CaptureSpeed;
            
            SetHitPoints(config.HitPoints);

            _hierarchy.Rigidbody.isKinematic = true;
            _hierarchy.Rigidbody.useGravity = false;
            _hierarchy.HitCollider.enabled = true;
        }
        public virtual void OnUpdate(float deltaTime)
        {
            if(_currentHitPoints == 0)
                return;
            
            if (!Mathf.Approximately(_frozenTime.Duration, 0f))
            {
                _frozenTime.Update();

                if (_frozenTime.IsEnded)
                    _frozenTime.Duration = 0;
                
                return;
            }
            
            _hierarchy.ZoneTransform.localScale += new Vector3(_captureSpeed, _captureSpeed) * deltaTime;
        }

        protected virtual void OnDealDamage(int damage)
        {
            SetHitPoints(Mathf.Max(_currentHitPoints - damage, 0));
            
            if (_currentHitPoints == 0)
            {
                _hierarchy.Rigidbody.isKinematic = false;
                _hierarchy.Rigidbody.useGravity = true;
                _hierarchy.HitCollider.enabled = false;
                _hierarchy.ZoneTransform.localScale = Vector3.one;
            }
        }
        private void OnSetFrozen(float frozenTime)
        {
            _frozenTime.Duration += frozenTime;
            _frozenTime.Reset();
        }
        private void Release()
        {
            _hierarchy.OnDealDamage -= OnDealDamage;
            _hierarchy.OnSetFrozen -= OnSetFrozen;
            _hierarchy.CollisionEnter -= OnCollisionEnter;
            
            OnRelease?.Invoke(this, _hierarchy);
        }
        protected void SetHitPoints(int hitPoints)
        {
            _currentHitPoints = hitPoints;
            _hierarchy.HitPointsText.text = hitPoints.ToString();
        }
        protected virtual void OnCollisionEnter(Collision collision)
        {
            if(!collision.gameObject.TryGetComponent<BulletHierarchy>(out var bullet))
                Release();
        }
    }
}