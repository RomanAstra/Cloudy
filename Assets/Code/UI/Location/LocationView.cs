using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cloudy.UI
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _needStars;
        [SerializeField] private TextMeshProUGUI _receivedStarsText;
        [SerializeField] private Button _chooseLocation;

        public void Initialize(ILocationViewModel viewModel)
        {
            _title.text = viewModel.Title;
            
            var isActive = string.IsNullOrEmpty(viewModel.NeedStarsToOpen);
            
            _needStars.gameObject.SetActive(!isActive);
            _needStars.text = viewModel.NeedStarsToOpen;
            _receivedStarsText.text = viewModel.Stars;
            _receivedStarsText.gameObject.SetActive(isActive);
            
            _chooseLocation.interactable = isActive;
            
            if (isActive)
            {
                _chooseLocation.onClick.RemoveListener(viewModel.ShowWeaponMenu);
                _chooseLocation.onClick.AddListener(viewModel.ShowWeaponMenu);
            }
        }
    }
}