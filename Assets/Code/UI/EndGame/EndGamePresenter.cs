using Cloudy;
using Configs.Upgrades.Weapons;
using Ui;
using Utils;

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
        private readonly OpenWeaponsPrefsController _openWeaponsIndexPrefsController;
        private readonly LocationsData _locationsData;
        private readonly int _percentToWin;

        public EndGamePresenter(IViewManager viewManager, GameManager gameManager, 
            WeaponsUpgradesPresenter weaponsUpgradesPresenter, WeaponUpgradeSystem weaponUpgradeSystem, 
            WeaponUpgradeProvider weaponUpgradeProvider, CloudZoneDetectorController detectorController, 
            WeaponsDataProvider weaponsDataProvider, OpenWeaponsPrefsController openWeaponsIndexPrefsController,
            LocationsData locationsData, int percentToWin) : base(viewManager)
        {
            _viewManager = viewManager;
            _gameManager = gameManager;
            _weaponsUpgradesPresenter = weaponsUpgradesPresenter;
            _weaponUpgradeSystem = weaponUpgradeSystem;
            _weaponUpgradeProvider = weaponUpgradeProvider;
            _detectorController = detectorController;
            _weaponsDataProvider = weaponsDataProvider;
            _openWeaponsIndexPrefsController = openWeaponsIndexPrefsController;
            _locationsData = locationsData;
            _percentToWin = percentToWin;
        }
        
        public override void Show()
        {
            var model = new EndGameViewModel(_gameManager, _weaponsUpgradesPresenter, _weaponUpgradeSystem,
                _weaponUpgradeProvider, _detectorController, _weaponsDataProvider, _openWeaponsIndexPrefsController, 
                _locationsData, _percentToWin);
            
            _viewManager.ShowWindow(model);
        }
    }
}