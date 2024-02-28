using TMPro;
using UnityEngine;

namespace Code.UI
{
    public sealed class OpenWeaponView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _openWeapon;

        public void Initialize(IOpenWeaponModel model)
        {
            var isActive = model != null;
            
            gameObject.SetActive(isActive);
            
            if(!isActive)
                return;
            
            _openWeapon.text = model.OpenWeapon;
        }
    }
}