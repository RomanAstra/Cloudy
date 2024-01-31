using UnityEngine;

namespace Cloudy
{
    public class Regenerating : Cloud
    {
        [Header("Shield")]
        [SerializeField] private GameObject _shield;
        [SerializeField] private Countdown _shieldDelay;
        [Header("Regeneration")]
        [SerializeField] private Countdown _regeniratingDelay;
        [SerializeField] private int _hitPointsRegeneratingCount;
        
        protected override void Update()
        {
            base.Update();
            
            _shieldDelay.Update();
            
            if(!_shieldDelay.IsEnded)
                return;
            
            _shieldDelay.Reset();
            ShowShield();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            
            _shieldDelay.CurrentTime = 0;
        }

        private void ShowShield()
        {
            _shield.SetActive(true);
        }
        private void HideShield()
        {
            _shield.SetActive(false);
        }
    }
}