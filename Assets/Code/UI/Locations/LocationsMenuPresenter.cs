using Cloudy;
using Ui;

namespace Code.UI
{
    public sealed class LocationsMenuPresenter : BasePresenter<LocationsMenuView, LocationsMenuViewModel>
    {
        private readonly IViewManager _viewManager;
        private readonly WeaponsMenuPresenter _weaponsMenuPresenter;
        private readonly LocationsData _locationsData;

        public LocationsMenuPresenter(IViewManager viewManager, WeaponsMenuPresenter weaponsMenuPresenter, 
            LocationsData locationsData) : base(viewManager)
        {
            _viewManager = viewManager;
            _weaponsMenuPresenter = weaponsMenuPresenter;
            _locationsData = locationsData;
        }
        

        public override void Show()
        {
            var locationsMenuViewModel = new LocationsMenuViewModel(_weaponsMenuPresenter, _locationsData);
            _viewManager.ShowWindow(locationsMenuViewModel);
        }
    }
}