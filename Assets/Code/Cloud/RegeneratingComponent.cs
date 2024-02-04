using UnityEngine;

namespace Cloudy
{
    public class RegeneratingComponent : MonoBehaviour
    {
        [SerializeField] private Cloud _cloud;
        [SerializeField] private Countdown _regeniratingDelay;
        [SerializeField] private int _hitPointsRegeneratingCount = 1;

        private void OnEnable()
        {
            _regeniratingDelay.Reset();
        }
        private void Update()
        {
            _regeniratingDelay.Update();
            
            if(!_regeniratingDelay.IsEnded)
                return;
            
            _cloud.AddHitPoints(_hitPointsRegeneratingCount);
            _regeniratingDelay.Reset();
        }
    }
}