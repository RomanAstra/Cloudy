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
        private readonly EndGameViewModel _model;

        public EndGamePresenter(IViewManager viewManager, GameManager gameManager, 
            WeaponsUpgradesPresenter weaponsUpgradesPresenter, WeaponUpgradeSystem weaponUpgradeSystem, 
            WeaponUpgradeProvider weaponUpgradeProvider, CloudZoneDetectorController detectorController, 
            WeaponsDataProvider weaponsDataProvider, SaveSystem saveSystem,
            LocationsData locationsData) : base(viewManager)
        {
            _viewManager = viewManager;
            
            _model = new EndGameViewModel(gameManager, weaponsUpgradesPresenter, weaponUpgradeSystem,
                weaponUpgradeProvider, detectorController, weaponsDataProvider, saveSystem, 
                locationsData);
        }
        
        public void Show()
        {
            _viewManager.ShowWindow(_model);
        }
    }
}