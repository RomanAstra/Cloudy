using UnityEngine;

namespace Cloudy
{
    public sealed class WeaponHierarchy : MonoBehaviour
    {
        public Transform FirePoint;

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
    }
}
