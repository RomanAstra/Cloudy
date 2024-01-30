using UnityEngine;
using Utils;

namespace Cloudy
{
    public class Cloud : MonoBehaviour
    {
        [SerializeField] private int _hitPoints;
        [SerializeField] private float _captureSpeed = 0.25f;
        [SerializeField] private Transform _zoneTransform;
        [SerializeField] private TextMesh _hitPointsText;

        private Pool<Cloud> _pool;
        private int _currentHitPoints;

        protected virtual void Update()
        {
            _zoneTransform.localScale += new Vector3(_captureSpeed, _captureSpeed) * Time.deltaTime;
        }
        protected virtual void OnEnable()
        {
            _currentHitPoints = _hitPoints;
            SetHitPoints(_hitPoints.ToString());
            _zoneTransform.localScale = Vector3.zero;
        }

        public void SetPool(Pool<Cloud> pool)
        {
            _pool = pool;
        }
        public void DealDamage(int damage)
        {
            _currentHitPoints -= damage;

            SetHitPoints(_currentHitPoints.ToString());
            if (_currentHitPoints <= 0)
            {
                _pool.Release(this);
                _pool = null;
            }
        }
        private void SetHitPoints(string hitPoints)
        {
            _hitPointsText.text = hitPoints;
        }
    }
}
