using TMPro;
using UnityEngine;

namespace Cloudy.UI
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private GameObject _indicator;

        public void Initialize(IWeaponViewModel viewModel)
        {
            _title.text = viewModel.WeaponName;
            _indicator.SetActive(viewModel.WithIndicator);
        }
    }
}