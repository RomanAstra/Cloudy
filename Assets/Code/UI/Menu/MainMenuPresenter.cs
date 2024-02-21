using Ui;
using UnityEngine;

namespace Code.UI
{
    public sealed class MainMenuPresenter //: BasePresenter<MainMenuView>
    {
        private readonly IViewManager _viewManager;

        public MainMenuPresenter(IViewManager viewManager)
        {
            _viewManager = viewManager;
            var mainMenuView = Object.FindFirstObjectByType<MainMenuView>();
            _viewManager.Register(mainMenuView);
        }
        
        public void Show()
        {
            MainMenuViewModel viewModel = new MainMenuViewModel();
            _viewManager.ShowWindow(viewModel);
        }
    }
}