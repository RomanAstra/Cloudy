using UnityEngine;
using UnityEngine.Serialization;
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
        private readonly Countdown _frozenTime = new ();
        private bool _isFrozen;
        
        protected virtual void Update()
        {
            if (_isFrozen)
            {
                _frozenTime.Update();

                if (_frozenTime.IsEnded)
                    _isFrozen = false;
                
                return;
            }
            
            _zoneTransform.localScale += new Vector3(_captureSpeed, _captureSpeed) * Time.deltaTime;
        }
        protected virtual void OnEnable()
        {
            _currentHitPoints = _hitPoints;
            SetHitPoints(_hitPoints.ToString());
            _zoneTransform.localScale = Vector3.zero;
            _isFrozen = false;
            _frozenTime.Duration = 0;
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
        public void AddHitPoints(int hitPoints)
        {
            if(_currentHitPoints == _hitPoints)
                return;

            _currentHitPoints = Mathf.Min(_currentHitPoints + hitPoints, _hitPoints);
            SetHitPoints(_currentHitPoints.ToString());
        }
        public void SetFrozen(float frozenTime)
        {
            _frozenTime.Duration += frozenTime;
            _frozenTime.Reset();
            _isFrozen = true;
        }
        
        private void SetHitPoints(string hitPoints)
        {
            _hitPointsText.text = hitPoints;
        }
    }
}
