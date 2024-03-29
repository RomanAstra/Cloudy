using Cloudy;
using Ui;

namespace Code.UI
{
    public sealed class WeaponsMenuPresenter : BasePresenter<WeaponsMenuView, WeaponsMenuViewModel>
    {
        private readonly IViewManager _viewManager;
        private readonly WeaponsDataProvider _dataWeaponsProvider;
        private readonly LocationsData _locationsData;

        public WeaponsMenuPresenter(IViewManager viewManager, WeaponsDataProvider dataWeaponsProvider, LocationsData locationsData) : base(viewManager)
        {
            _viewManager = viewManager;
            _dataWeaponsProvider = dataWeaponsProvider;
            _locationsData = locationsData;
        }
        
        public void Show()
        {
            var mainMenuViewModel = new WeaponsMenuViewModel(_dataWeaponsProvider, _locationsData);
            _viewManager.ShowWindow(mainMenuViewModel);
        }
    }
}