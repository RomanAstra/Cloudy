using System;
using UnityEngine;

namespace Cloudy
{
    public class CloudHierarchy : MonoBehaviour
    {
        public Transform ZoneTransform;
        public TextMesh HitPointsText;
        public Rigidbody Rigidbody;
        public Collider HitCollider;
        public AudioClip ExplosionAudioClip;

        public event Action<int> OnDealDamage;
        public event Action<float> OnSetFrozen;
        public event Action<Collision> CollisionEnter;
        
        private void OnCollisionEnter(Collision other)
        {
            CollisionEnter?.Invoke(other);
        }
        private void OnDestroy()
        {
            OnDealDamage = null;
            OnSetFrozen = null;
        }

        public void DealDamage(int damage)
        {
            OnDealDamage?.Invoke(damage);
        }
        public void SetFrozen(float frozenTime)
        {
            OnSetFrozen?.Invoke(frozenTime);
        }
    }
}
