using System;
using Cloudy;
using Configs.Upgrades.Weapons;
using Ui;
using UnityEngine.SceneManagement;
using Utils;

namespace Code.UI
{
    public sealed class EndGameViewModel : IEndGameViewModel
    {
        private readonly GameManager _gameManager;
        private readonly WeaponsUpgradesPresenter _weaponsUpgradesPresenter;
        private readonly WeaponUpgradeSystem _weaponUpgradeSystem;
        private readonly WeaponUpgradeProvider _weaponUpgradeProvider;
        private readonly CloudZoneDetectorController _detectorController;
        private readonly WeaponsDataProvider _dataWeaponsProvider;
        private readonly OpenWeaponsPrefsController _openWeaponsIndexPrefsController;
        private readonly LocationsData _locationsData;
        private readonly int _percentToWin;

        public event Action<IViewModel> OnClosed;

        public IOpenWeaponModel OpenWeaponModel { get; private set; }
        public ILevelProgressModel ProgressLevelModel { get; private set; }
        public string ResultGame { get; private set; }
        public bool CanContinue { get; private set; }

        public EndGameViewModel(GameManager gameManager, WeaponsUpgradesPresenter weaponsUpgradesPresenter, 
            WeaponUpgradeSystem weaponUpgradeSystem, WeaponUpgradeProvider weaponUpgradeProvider, 
            CloudZoneDetectorController detectorController, WeaponsDataProvider dataWeaponsProvider,
            OpenWeaponsPrefsController openWeaponsIndexPrefsController, LocationsData locationsData, int percentToWin)
        {
            _gameManager = gameManager;
            _weaponsUpgradesPresenter = weaponsUpgradesPresenter;
            _weaponUpgradeSystem = weaponUpgradeSystem;
            _weaponUpgradeProvider = weaponUpgradeProvider;
            _detectorController = detectorController;
            _dataWeaponsProvider = dataWeaponsProvider;
            _openWeaponsIndexPrefsController = openWeaponsIndexPrefsController;
            _locationsData = locationsData;
            _percentToWin = percentToWin;
        }

        public void Show()
        {
            var percent = _detectorController.GetZoneProtectionProgress();
            var isWin = percent > _percentToWin;
            
            ResultGame = isWin ? "Победа" : "Поражение";
            
            var weaponCount = _openWeaponsIndexPrefsController.GetCount();
            if (isWin && _locationsData.IsMaxLevel && _locationsData.CurrentLocation == weaponCount)
            {
                weaponCount++;
                _openWeaponsIndexPrefsController.SaveCount(weaponCount);

                OpenWeaponModel = new OpenWeaponModel($"Новое оружие {_dataWeaponsProvider.GetWeapon(weaponCount - 1)}");
            }

            ProgressLevelModel = new LevelProgressModel(percent);

            CanContinue = isWin && !_locationsData.IsMaxLevel;
            
            _gameManager.SetState(GameState.FINISHED);
        }
        public void ShowWeaponsUpgrades()
        {
            _weaponsUpgradesPresenter.Show();
            _locationsData.CurrentLevel++;
        }
        public void RestartGame()
        {
            _locationsData.CurrentLevel = 1;
            ResetUpgrades();
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
        public void ExitGame()
        {
            ResetUpgrades();
            SceneManager.LoadScene("Menu");
        }
        public void Close()
        {
            OnClosed?.Invoke(this);
        }

        private void ResetUpgrades()
        {
            _weaponUpgradeProvider.Reset();
            _weaponUpgradeSystem.Reset();
        }
    }
}