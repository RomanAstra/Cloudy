using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cloudy
{
    public sealed class Timer : MonoBehaviour
    {
        [SerializeField] private Countdown _roundTime = new (60, 60);
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private CloudZoneDetectorController _detectorController;
        
        private void Update()
        {
            _roundTime.Update();

            _timerText.text = _roundTime.CurrentTime.ToString("00");
            
            if(!_roundTime.IsEnded)
                return;

            var percent = _detectorController.GetZoneCaptureProgress();
            Debug.Log($"{percent}% zone protected");
            SceneManager.LoadScene(0);
        }
    }
}