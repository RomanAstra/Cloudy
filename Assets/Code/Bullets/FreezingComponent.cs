using UnityEngine;

namespace Cloudy
{
    public sealed class FreezingComponent : MonoBehaviour
    {
        [SerializeField] private float _frozenTime = 3f;
        
        private void OnTriggerEnter(Collider other)
        {
            var cloud = other.GetComponentInParent<Cloud>();
            if (cloud == null)
                return;

            cloud.SetFrozen(_frozenTime);
        }
    }
}