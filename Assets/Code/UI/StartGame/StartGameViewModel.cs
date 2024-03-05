using System;
using Ui;
using Utils;

namespace Code.UI
{
    public sealed class StartGameViewModel : IStartGameViewModel
    {
        private readonly GameManager _gameManager;
        
        public event Action<IViewModel> OnClosed;

        public StartGameViewModel(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        public void StartGame()
        {
            _gameManager.SetState(GameState.PLAYING);
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
