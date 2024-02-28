using TMPro;
using UnityEngine;

namespace Cloudy.UI
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;

        public void SetView(string weaponName)
        {
            _title.text = weaponName;
        }
    }
}