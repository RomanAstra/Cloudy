using Configs.Upgrades.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
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

        private OpenWeaponsIndexPrefsController _openWeaponsIndexPrefsController;
        private WeaponUpgradeSystem _weaponUpgradeSystem;
        private WeaponUpgradeProvider _weaponUpgradeProvider;
        private CloudZoneDetectorController _detectorController;
        private GameManager _gameManager;
        private bool _isWin;

        [Inject]
        public void Construct(CloudZoneDetectorController detectorController, GameManager gameManager, 
            WeaponUpgradeSystem weaponUpgradeSystem, WeaponUpgradeProvider weaponUpgradeProvider,
            OpenWeaponsIndexPrefsController openWeaponsIndexPrefsController)
        {
            _openWeaponsIndexPrefsController = openWeaponsIndexPrefsController;
            _weaponUpgradeSystem = weaponUpgradeSystem;
            _weaponUpgradeProvider = weaponUpgradeProvider;
            _detectorController = detectorController;
            _detectorController.AllZonesСaptured += OnAllZonesСaptured;

            _gameManager = gameManager;

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
            _isWin = isWin;
            gameObject.SetActive(true);
            _resultGameText.text = _isWin ? "Победа" : "Поражение";
            var weaponIndex = _openWeaponsIndexPrefsController.GetIndex();
            if (_isWin && App.IsMaxLevel && App.CurrentLocation == weaponIndex + 1)
            {
                weaponIndex++;
                _openWeaponsIndexPrefsController.SaveIndex(weaponIndex);
                _openedWeaponText.gameObject.SetActive(true);
                _openedWeaponText.text = $"Новое оружие {App.Weapons[weaponIndex]}";
            }

            if (!_isWin)
            {
                _weaponUpgradeProvider.Reset();
                _weaponUpgradeSystem.Reset();
            }
            
            _continurButton.gameObject.SetActive(_isWin && !App.IsMaxLevel);

            _progressBarImage.fillAmount = _detectorController.GetZoneProtectionProgress() / 100f;
            
            _gameManager.SetState(GameState.FINISHED);
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
            _weaponsUpgradePopup.Show();
            App.CurrentLevel++;
            Hide();
        }
    }
}