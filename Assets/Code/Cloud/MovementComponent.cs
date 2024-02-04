using DG.Tweening;
using UnityEngine;

namespace Cloudy
{
    public sealed class MovementComponent : MonoBehaviour
    {
        [SerializeField] private Transform _bodTransform;
        [SerializeField] private Vector3 _targetPoint;
        [SerializeField] private float _duration = 1f;
        
        private Tweener _tween;

        private void OnEnable()
        {
            _tween = _bodTransform.DOLocalMove(_targetPoint, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).
                OnComplete(() => _bodTransform.position = Vector3.zero);
        }
        private void OnDisable()
        {
            _tween?.Kill(true);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, transform.position + _targetPoint);
        }
    }
}