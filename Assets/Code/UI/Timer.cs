using System;
using Cloudy.UI;
using TMPro;
using UnityEngine;

namespace Cloudy
{
    public sealed class Timer : MonoBehaviour
    {
        [SerializeField] private Countdown _roundTime = new (60, 60);
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private CloudZoneDetectorController _detectorController;
        [SerializeField] private EndGamePopup _endGamePopup;

        private void Awake()
        {
            Time.timeScale = 1;
        }
        private void Update()
        {
            if(_endGamePopup.gameObject.activeSelf)
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