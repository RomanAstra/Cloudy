using Cloudy.UI.Screens;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cloudy.UI
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Button _chooseLocation;

        public void SetView(string locationName, int numberLocation)
        {
            _title.text = locationName;
            
            _chooseLocation.onClick.AddListener(() =>
            {
                App.CurrentLocation = numberLocation;
                ScreenManager.Show<WeaponsScreen>();
            });
        }
    }
}