using Cloudy;
using Cloudy.SaveData;
using Ui;

namespace Code.UI
{
    public sealed class StarsProgressPresenter : BasePresenter<StarsProgressView, StarsProgressViewModel>
    {
        private readonly IViewManager _viewManager;
        private readonly WeaponsMenuPresenter _weaponsMenuPresenter;
        private readonly OpenObjectStarsConfig _openObjectStarsConfig;
        private readonly WeaponsDataProvider _weaponsDataProvider;
        private readonly LocationsData _locationsData;
        private readonly SaveSystem _saveSystem;

        public StarsProgressPresenter(IViewManager viewManager, LocationsData locationsData, 
            OpenObjectStarsConfig openObjectStarsConfig, WeaponsDataProvider weaponsDataProvider, 
            SaveSystem saveSystem) : 
            base(viewManager)
        {
            _viewManager = viewManager;
            _openObjectStarsConfig = openObjectStarsConfig;
            _weaponsDataProvider = weaponsDataProvider;
            _locationsData = locationsData;
            _saveSystem = saveSystem;
        }
        

        public override void Show()
        {
            var model = new StarsProgressViewModel(_locationsData, _openObjectStarsConfig,
                _weaponsDataProvider, _saveSystem);
            _viewManager.ShowWindowOutOfStack(model);
        }
    }
}