using Cloudy.UI;
using TMPro;
using UnityEngine;
using Utils;
using Zenject;

namespace Cloudy
{
    public sealed class Timer : MonoBehaviour, IUpdate
    {
        [SerializeField] private Countdown _roundTime = new (60, 60);
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private EndGamePopup _endGamePopup;
        
        private CloudZoneDetectorController _detectorController;

        [Inject]
        private void Construct(CloudZoneDetectorController detectorController)
        {
            _detectorController = detectorController;
        }
        
        public void OnUpdate(float deltaTime)
        {
            if(_roundTime.IsEnded)
                return;
            
            _roundTime.Update();

            _timerText.text = _roundTime.CurrentTime.ToString("00");
            
            if(!_roundTime.IsEnded)
                return;

            var percent = _detectorController.GetZoneProtectionProgress();
            Debug.Log($"Zone protected {percent}");
            _endGamePopup.Show(percent > 50);
        }
    }
}