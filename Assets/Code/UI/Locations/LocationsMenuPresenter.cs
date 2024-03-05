using Cloudy;
using Cloudy.SaveData;
using Ui;

namespace Code.UI
{
    public sealed class LocationsMenuPresenter : BasePresenter<LocationsMenuView, LocationsMenuViewModel>
    {
        private readonly IViewManager _viewManager;
        private readonly WeaponsMenuPresenter _weaponsMenuPresenter;
        private readonly OpenObjectStarsConfig _openObjectStarsConfig;
        private readonly LocationsData _locationsData;
        private readonly SaveSystem _saveSystem;

        public LocationsMenuPresenter(IViewManager viewManager, WeaponsMenuPresenter weaponsMenuPresenter, 
            OpenObjectStarsConfig openObjectStarsConfig, LocationsData locationsData, SaveSystem saveSystem) : 
            base(viewManager)
        {
            _viewManager = viewManager;
            _weaponsMenuPresenter = weaponsMenuPresenter;
            _openObjectStarsConfig = openObjectStarsConfig;
            _locationsData = locationsData;
            _saveSystem = saveSystem;
        }
        

        public override void Show()
        {
            var locationsMenuViewModel = new LocationsMenuViewModel(_weaponsMenuPresenter, _locationsData, 
                _openObjectStarsConfig, _saveSystem);
            _viewManager.ShowWindow(locationsMenuViewModel);
        }
    }
}