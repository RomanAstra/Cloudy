using System;
using Ui;

namespace Code.UI
{
    public sealed class MainMenuViewModel : IMainMenuViewModel
    {
        private readonly LocationsMenuPresenter _locationsMenuPresenter;
        public string Title => "Меню";
        public event Action<IViewModel> OnClosed;

        public MainMenuViewModel(LocationsMenuPresenter locationsMenuPresenter)
        {
            _locationsMenuPresenter = locationsMenuPresenter;
        }
        
        public void StartGame()
        {
            _locationsMenuPresenter.Show();
        }
        
        public void ShowSettings()
        {
            
        }

        public void Close()
        {
            OnClosed?.Invoke(this);
        }
    }
}
