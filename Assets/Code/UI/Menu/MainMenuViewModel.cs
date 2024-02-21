using System;
using Ui;

namespace Code.UI
{
    public sealed class MainMenuViewModel : IMainMenuViewModel
    {
        public string Title { get; }
        public event Action<IViewModel> OnClosed;
        public void StartGame()
        {
            
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
