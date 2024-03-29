using Ui;

namespace Code.UI
{
    public sealed class MainMenuPresenter : BasePresenter<MainMenuView, MainMenuViewModel>
    {
        private readonly LocationsMenuPresenter _locationsMenuPresenter;
        private readonly IViewManager _viewManager;

        public MainMenuPresenter(IViewManager viewManager, LocationsMenuPresenter locationsMenuPresenter) :  base(viewManager)
        {
            _viewManager = viewManager;
            _locationsMenuPresenter = locationsMenuPresenter;
        }

        public void Show()
        {
            var mainMenuViewModel = new MainMenuViewModel(_locationsMenuPresenter);
            _viewManager.ShowWindow(mainMenuViewModel);
        }
    }
}