using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Cloudy.UI
{
    public sealed class EndGamePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _resultGameText;
        [SerializeField] private TextMeshProUGUI _openedWeaponText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _continurButton;
        [SerializeField] private Image _progressBarImage;
        [SerializeField] private WeaponsUpgradePopup _weaponsUpgradePopup;

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
            gameObject.SetActive(true);
            _resultGameText.text = isWin ? "Победа" : "Поражение";
            
            if (isWin && App.IsMaxLevel && App.CurrentLocation == App.OpenWeaponIndex + 1)
            {
                App.OpenWeaponIndex++;
                _openedWeaponText.gameObject.SetActive(true);
                _openedWeaponText.text = $"Новое оружие {App.Weapons[App.OpenWeaponIndex]}";
            }

            _continurButton.gameObject.SetActive(isWin && !App.IsMaxLevel);

            _progressBarImage.fillAmount = _detectorController.GetZoneProtectionProgress() / 100f;
            
            if(!isWin)
                WeaponUpgradeSystem.Reset();
            
            Time.timeScale = 0;
        }
        public void Hide()
        {
            gameObject.SetActive(false);
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
            _weaponsUpgradePopup.Show();
            Hide();
        }
    }
}