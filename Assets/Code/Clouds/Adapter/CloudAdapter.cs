using System;
using Cloudy.Configs;
using UnityEngine;
using Utils;

namespace Cloudy.Adapter
{
    public class CloudAdapter : IUpdate
    {
        private readonly CloudHierarchy _hierarchy;
        private readonly CloudConfig _config;
        private readonly float _captureSpeed;
        protected int _currentHitPoints;
        private readonly Countdown _frozenTime = new ();
        
        public event Action<CloudAdapter, CloudHierarchy> OnRelease;

        public CloudAdapter(CloudHierarchy hierarchy, CloudConfig config)
        {
            _hierarchy = hierarchy;
            _config = config;

            _hierarchy.OnDealDamage += OnDealDamage;
            _hierarchy.OnSetFrozen += OnSetFrozen;

            _captureSpeed = _config.CaptureSpeed;
            
            _currentHitPoints = _config.HitPoints;
            SetHitPoints(_currentHitPoints);
           
            _hierarchy.ZoneTransform.localScale = Vector3.one;
        }
        public virtual void OnUpdate(float deltaTime)
        {
            if (!Mathf.Approximately(_frozenTime.Duration, 0f))
            {
                _frozenTime.Update();

                if (_frozenTime.IsEnded)
                    _frozenTime.Duration = 0;
                
                return;
            }
            
            _hierarchy.ZoneTransform.localScale += new Vector3(_captureSpeed, _captureSpeed) * deltaTime;
        }

        private void OnDealDamage(int damage)
        {
            _currentHitPoints -= damage;

            SetHitPoints(_currentHitPoints);
            if (_currentHitPoints <= 0)
            {
                Release();
            }
        }
        private void OnSetFrozen(float frozenTime)
        {
            _frozenTime.Duration += frozenTime;
            _frozenTime.Reset();
        }
        protected virtual void Release()
        {
            _hierarchy.OnDealDamage -= OnDealDamage;
            _hierarchy.OnSetFrozen -= OnSetFrozen;
            
            OnRelease?.Invoke(this, _hierarchy);
        }
        protected void SetHitPoints(int hitPoints)
        {
            _hierarchy.HitPointsText.text = hitPoints.ToString();
        }
    }
}