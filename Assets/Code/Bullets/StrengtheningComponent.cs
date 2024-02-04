using UnityEngine;

namespace Cloudy
{
    public class StrengtheningComponent : MonoBehaviour
    {
        private int _damage;

        private void OnEnable()
        {
            _damage = 0;
        }
        private void OnTriggerEnter(Collider other)
        {
            var cloud = other.GetComponentInParent<Cloud>();
            if (cloud == null)
                return;
            
            cloud.DealDamage(_damage);
            _damage++;
        }
    }
}