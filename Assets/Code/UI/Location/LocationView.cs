using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cloudy.UI
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Button _chooseLocation;

        public void Initialize(ILocationViewModel viewModel)
        {
            _title.text = viewModel.Title;
            
            _chooseLocation.onClick.RemoveListener(viewModel.ShowWeaponMenu);
            _chooseLocation.onClick.AddListener(viewModel.ShowWeaponMenu);
        }
    }
}