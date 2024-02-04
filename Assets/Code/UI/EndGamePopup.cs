using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Cloudy.UI
{
    public class EndGamePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _resultGameText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _continurButton;
        [SerializeField] private Image _progressBarImage;
        
        private CloudZoneDetectorController _detectorController;

        [Inject]
        public void Construct(CloudZoneDetectorController detectorController)
        {
            _detectorController = detectorController;
            _detectorController.AllZonesСaptured += OnAllZonesСaptured;
            
            _restartButton.onClick.AddListener(RestartGame);
            _exitButton.onClick.AddListener(ExitGame);
            _continurButton.onClick.AddListener(ContinueGame);
        }
        private void OnAllZonesСaptured()
        {
            Show(false);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _continurButton.onClick.RemoveAllListeners();
        }

        public void Show(bool isWin)
        {
            Time.timeScale = 0;
            
            gameObject.SetActive(true);
            _resultGameText.text = isWin ? "Победа" : "Поражение";
            
            _continurButton.gameObject.SetActive(isWin && !App.IsMaxLevel);
            
            _progressBarImage.fillAmount = _detectorController.GetZoneProtectionProgress() / 100f;
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
        private void ExitGame()
        {
            SceneManager.LoadScene("Menu");
        }
        private void ContinueGame()
        {
            App.CurrentLevel++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
    }
}