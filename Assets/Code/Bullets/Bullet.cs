using System;
using UnityEngine;
using Utils;

namespace Cloudy
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed = 10;
        [SerializeField] private int _ricochetCount = -1;
        [SerializeField] private int _targetCount = -1;
        [SerializeField] private Countdown _lifeTime;
        [SerializeField] private Rigidbody _rigidbody;

        private Pool<Bullet> _pool;
        private Vector3 _moveDirection;
        private int _currentRicochetCount;
        private int _currentTargetCount;

        private void Update()
        {
            if (_lifeTime.IsInfinitely)
                return;

            _lifeTime.Update();

            if (_lifeTime.IsEnded)
                Release();
        }
        private void OnCollisionEnter(Collision other)
        {
            if (_ricochetCount < 0)
                return;

            _currentRicochetCount++;

            if (_currentRicochetCount > _ricochetCount)
                Release();
        }
        private void OnTriggerEnter(Collider other)
        {
            _currentTargetCount++;
            var cloud = other.GetComponentInParent<Cloud>();
            //if (other.gameObject.TryGetComponent<Cloud>(out var cloud))
            if (cloud == null)
                return;
            
            cloud.DealDamage(_damage);

            if (_targetCount > 0 && _currentTargetCount >= _targetCount)
                Release();
        }
        private void OnEnable()
        {
            _currentRicochetCount = 0;
            _currentTargetCount = 0;
            _lifeTime.Reset();
        }

        public void SetPool(Pool<Bullet> pool)
        {
            _pool = pool;
        }
        public void Move(Vector3 direction)
        {
            _rigidbody.velocity = _speed * direction;
        }

        private void Release()
        {
            _pool?.Release(this);
            _pool = null;
        }
    }
}