using Cloudy.Configs;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Cloudy.Adapter
{
    public sealed class ImmortalCloudAdapter : CloudAdapter, IGameFinish
    {
        private readonly ImmortalCloudHierarchy _hierarchy;
        private readonly ImmortalCloudConfig _config;
        private readonly Countdown _shieldTime = new();
        private readonly Countdown _shieldDelay = new();
        private readonly float _startShieldAlpha;
        private Tweener _tween;

        public ImmortalCloudAdapter(ImmortalCloudHierarchy hierarchy, ImmortalCloudConfig config) : base(hierarchy, config)
        {
            _hierarchy = hierarchy;
            _config = config;

            _shieldTime.Duration = config.ShieldTime;
            _shieldDelay.Duration = config.ShieldDelay;
            
            _startShieldAlpha = _hierarchy.Shield.color.a;
        }
        
        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            
            _shieldTime.Update();
            
            if(!_shieldTime.IsEnded)
                return;

            HideShield();
            _shieldDelay.Update();
            
            if(!_shieldDelay.IsEnded)
                return;
            
            _shieldTime.Reset();
            _shieldDelay.Reset();
            ShowShield();
        }
        public void OnFinish()
        {
            _tween?.Kill();
        }

        protected override void Release()
        {
            base.Release();
            
            _tween?.Kill(true);
        }

        private void ShowShield()
        {
            _tween?.Kill();
            var color = _hierarchy.Shield.color;
            color.a = _startShieldAlpha;
            _hierarchy.Shield.color = color;
            
            _hierarchy.Shield.gameObject.SetActive(true);
            _tween = _hierarchy.Shield.DOFade(_config.ShieldAlpha, _config.ShieldBlinkDelay).
                SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InExpo);
        }
        private void HideShield()
        {
            _tween?.Kill(true);
            _hierarchy.Shield.gameObject.SetActive(false);
        }
    }
}