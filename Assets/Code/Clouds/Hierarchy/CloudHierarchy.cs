using System;
using UnityEngine;

namespace Cloudy
{
    public class CloudHierarchy : MonoBehaviour
    {
        public Transform ZoneTransform;
        public TextMesh HitPointsText;

        public event Action<int> OnDealDamage;
        public event Action<float> OnSetFrozen;

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
