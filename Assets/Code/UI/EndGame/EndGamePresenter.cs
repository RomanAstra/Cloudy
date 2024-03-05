using Cloudy;
using Cloudy.SaveData;
using Configs.Upgrades.Weapons;
using Ui;
using Utils;
using LocationsData = Cloudy.LocationsData;

namespace Code.UI
{
    public sealed class EndGamePresenter : BasePresenter<EndGameView, EndGameViewModel>
    {
        private readonly IViewManager _viewManager;
        private readonly GameManager _gameManager;
        private readonly WeaponsUpgradesPresenter _weaponsUpgradesPresenter;
        private readonly WeaponUpgradeSystem _weaponUpgradeSystem;
        private readonly WeaponUpgradeProvider _weaponUpgradeProvider;
        private readonly CloudZoneDetectorController _detectorController;
        private readonly WeaponsDataProvider _weaponsDataProvider;
        private readonly SaveSystem _saveSystem;
        private readonly LocationsData _locationsData;

        public EndGamePresenter(IViewManager viewManager, GameManager gameManager, 
            WeaponsUpgradesPresenter weaponsUpgradesPresenter, WeaponUpgradeSystem weaponUpgradeSystem, 
            WeaponUpgradeProvider weaponUpgradeProvider, CloudZoneDetectorController detectorController, 
            WeaponsDataProvider weaponsDataProvider, SaveSystem saveSystem,
            LocationsData locationsData) : base(viewManager)
        {
            _viewManager = viewManager;
            _gameManager = gameManager;
            _weaponsUpgradesPresenter = weaponsUpgradesPresenter;
            _weaponUpgradeSystem = weaponUpgradeSystem;
            _weaponUpgradeProvider = weaponUpgradeProvider;
            _detectorController = detectorController;
            _weaponsDataProvider = weaponsDataProvider;
            _saveSystem = saveSystem;
            _locationsData = locationsData;
        }
        
        public override void Show()
        {
            var model = new EndGameViewModel(_gameManager, _weaponsUpgradesPresenter, _weaponUpgradeSystem,
                _weaponUpgradeProvider, _detectorController, _weaponsDataProvider, _saveSystem, 
                _locationsData);
            
            _viewManager.ShowWindow(model);
        }
    }
}