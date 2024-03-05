using System;
using Cloudy;
using Cloudy.SaveData;
using Configs.Upgrades.Weapons;
using Ui;
using UnityEngine.SceneManagement;
using Utils;
using YG;

namespace Code.UI
{
    public sealed class EndGameViewModel : IEndGameViewModel
    {
        private readonly GameManager _gameManager;
        private readonly WeaponsUpgradesPresenter _weaponsUpgradesPresenter;
        private readonly WeaponUpgradeSystem _weaponUpgradeSystem;
        private readonly WeaponUpgradeProvider _weaponUpgradeProvider;
        private readonly CloudZoneDetectorController _detectorController;
        private readonly WeaponsDataProvider _weaponsDataProvider;
        private readonly SaveSystem _saveSystem;
        private readonly LocationsData _locationsData;

        private int _starsCount;
        
        public ILevelProgressModel ProgressLevelModel { get; private set; }
        public string ResultGame { get; private set; }
        public bool CanContinue { get; private set; }
        public bool CanShowAds { get; private set; }

        public event Action<IViewModel> OnClosed;
        
        public EndGameViewModel(GameManager gameManager, WeaponsUpgradesPresenter weaponsUpgradesPresenter, 
            WeaponUpgradeSystem weaponUpgradeSystem, WeaponUpgradeProvider weaponUpgradeProvider, 
            CloudZoneDetectorController detectorController, WeaponsDataProvider dataWeaponsProvider,
            SaveSystem saveSystem, LocationsData locationsData)
        {
            _gameManager = gameManager;
            _weaponsUpgradesPresenter = weaponsUpgradesPresenter;
            _weaponUpgradeSystem = weaponUpgradeSystem;
            _weaponUpgradeProvider = weaponUpgradeProvider;
            _detectorController = detectorController;
            _weaponsDataProvider = dataWeaponsProvider;
            _saveSystem = saveSystem;
            _locationsData = locationsData;
        }

        public void Show()
        {
            var percent = _detectorController.GetZoneProtectionProgress();
            var isWin = _locationsData.IsWin(percent);
            
            ResultGame = isWin ? "Победа" : "Поражение";
            
            if (isWin)
            {
                _starsCount = _locationsData.GetStars(percent);
                _locationsData.CurrentStars += _starsCount;

                if (_locationsData.IsMaxLevel)
                    SaveLocationResult();
            }

            ProgressLevelModel = new LevelProgressModel(percent);

            CanContinue = isWin && !_locationsData.IsMaxLevel;
            CanShowAds = percent is > 0 and < 90;
            
            _gameManager.SetState(GameState.PAUSED);
        }
        public void ContinueGame()
        {
            _locationsData.CurrentLevel++;
            _weaponsUpgradesPresenter.Show();
        }
        public void RestartGame()
        {
            _locationsData.CurrentLevel = 1;
            _locationsData.CurrentStars = 0;
            
            YandexGame.FullscreenShow();
            ResetUpgrades();
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
        public void ExitGame()
        {
            _locationsData.CurrentLevel = 1;
            YandexGame.FullscreenShow();
            ResetUpgrades();
            SceneManager.LoadScene("Menu");
        }
        public void Close()
        {
            OnClosed?.Invoke(this);
        }
        public void ShowAds()
        {
            _locationsData.CurrentStars -= _starsCount;
            YandexGame.RewVideoShow(0);
        }
        public void GameResume()
        {
            _gameManager.SetState(GameState.PLAYING);
        }
        
        private void ResetUpgrades()
        {
            _weaponUpgradeProvider.Reset();
            _weaponUpgradeSystem.Reset();
            _locationsData.CurrentStars = 0;
        }
        private void SaveLocationResult()
        {
            var location = _saveSystem.SaveData.GetLocationData(_locationsData.CurrentLocation);
                
            var currentWeapons = _weaponsDataProvider.GetCurrentWeapons(); 
            
            var needSave = false;
            for (var i = 0; i < currentWeapons.Count; i++)
            {
                var weapon = currentWeapons[i];
                if (!location.Weapons.Contains(weapon))
                {
                    needSave = true;
                    location.Weapons.Add(weapon);
                }
            }

            if (location.Stars < _locationsData.CurrentStars)
            {
                location.Stars = _locationsData.CurrentStars;

                var starsCount = _saveSystem.SaveData.GetStarsCount();
                var weapons = _weaponsDataProvider.GetWeapons();
                
                for (var i = 0; i < weapons.Count; i++)
                {
                    var weapon = weapons[i];

                    if (weapon.StarsCount <= starsCount && !_weaponsDataProvider.CheckOpenedWeapons(weapon.Id))
                        _weaponsDataProvider.AddOpenWeapon(weapon);
                }
                needSave = true;
            }
            
            if(needSave)
                _saveSystem.Save();
        }
    }
}