using System;
using UnityEngine;

namespace Cloudy
{
    [RequireComponent(typeof(Collider))]
    public sealed class ShieldComponent : MonoBehaviour
    {
        public event Action onCollisionEnter;
        
        private void OnCollisionEnter()
        {
            onCollisionEnter?.Invoke();
        }
    }
}