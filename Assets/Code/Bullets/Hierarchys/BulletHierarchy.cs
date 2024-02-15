using System;
using Configs.Upgrades;
using UnityEngine;

namespace Cloudy
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public sealed class BulletHierarchy : MonoBehaviour
    {
        public Rigidbody Rigidbody;

        public event Action<Collision> onCollisionEnter;
        public event Action<CloudHierarchy> onTriggerEnter;
        
        private void OnCollisionEnter(Collision other)
        {
            onCollisionEnter?.Invoke(other);
        }
        private void OnTriggerEnter(Collider other)
        {
            var cloud = other.GetComponentInParent<CloudHierarchy>();
            if (cloud == null)
                return;
            
            onTriggerEnter?.Invoke(cloud);
        }
    }
}