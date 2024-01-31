using DG.Tweening;
using UnityEngine;

namespace Cloudy
{
    public class ImmortalCloud : Cloud
    {
        [Header("Shield")]
        [SerializeField] private GameObject _shield;
        [SerializeField] private Countdown _shieldTime;
        [SerializeField] private Countdown _shieldDelay;
        [SerializeField] private float _scaleShieldCoefficient = 1.1f;
        [SerializeField] private float _shieldBlinkDelay = 0.2f;

        private Vector3 _shieldScale;
        private Tweener _tween;

        private void Awake()
        {
            _shieldScale = _shield.gameObject.transform.localScale;
        }
        protected override void Update()
        {
            base.Update();
            
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
        protected override void OnEnable()
        {
            base.OnEnable();
            
            _shieldTime.CurrentTime = 0;
            _shieldDelay.CurrentTime = 0;
        }
        private void OnDestroy()
        {
            _tween.Kill();
        }

        private void ShowShield()
        {
            _tween?.Kill(true);
            _shield.SetActive(true);
            _shield.gameObject.transform.localScale = _shieldScale;
            _tween = _shield.gameObject.transform.DOScale(_shieldScale * _scaleShieldCoefficient, _shieldBlinkDelay).
                SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InQuart);
        }
        private void HideShield()
        {
            _shield.SetActive(false);
        }
    }
}