using UnityEngine;
using UnityEngine.UI;

namespace Cloudy
{
    public sealed class ZoneCaptureProgress : MonoBehaviour
    {
        [SerializeField] private Image _progressBarImage;
        [SerializeField] private CloudZoneDetectorController _detectorController;
        
        private void Update()
        {
            _progressBarImage.fillAmount = _detectorController.GetZoneProtectionProgress() / 100f;
        }
    }
}