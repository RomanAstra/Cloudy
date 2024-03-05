using Cloudy.Configs;
using DG.Tweening;

namespace Cloudy.Adapter
{
    public sealed class ImmortalCloudAdapter : CloudAdapter
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
            
            if(_currentHitPoints == 0)
                return;
            
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

        protected override void OnDealDamage(int damage)
        {
            base.OnDealDamage(damage);
            if(_currentHitPoints == 0)
                _tween?.Kill(true);
        }

        private void ShowShield()
        {
            _tween?.Kill();
            var color = _hierarchy.Shield.color;
            color.a = _startShieldAlpha;
            _hierarchy.Shield.color = color;

            var shield = _hierarchy.Shield.gameObject;
            shield.SetActive(true);
            _tween = _hierarchy.Shield.DOFade(_config.ShieldAlpha, _config.ShieldBlinkDelay).
                SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InExpo).SetLink(shield);
            _hierarchy.HitCollider.enabled = false;
        }
        private void HideShield()
        {
            _tween?.Kill(true);
            _hierarchy.Shield.gameObject.SetActive(false);
            _hierarchy.HitCollider.enabled = true;
        }
    }
}