using Ui;

namespace Code.UI
{
    public sealed class WeaponsMenuPresenter : BasePresenter<WeaponsMenuView, WeaponsMenuViewModel>
    {
        private readonly IViewManager _viewManager;

        public WeaponsMenuPresenter(IViewManager viewManager) : base(viewManager)
        {
            _viewManager = viewManager;
        }
        
        public override void Show()
        {
            var mainMenuViewModel = new WeaponsMenuViewModel();
            _viewManager.ShowWindow(mainMenuViewModel);
        }
    }
}