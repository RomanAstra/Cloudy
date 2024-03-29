using System;
using Ui;
using Utils;

namespace Code.UI
{
    public sealed class StartGameViewModel : IStartGameViewModel
    {
        private readonly GameManager _gameManager;
        private readonly TimerPresenter _timerPresenter;

        public event Action<IViewModel> OnClosed;

        public StartGameViewModel(GameManager gameManager, TimerPresenter timerPresenter)
        {
            _gameManager = gameManager;
            _timerPresenter = timerPresenter;
        }
        
        public void StartGame()
        {
            _timerPresenter.Show();
            _gameManager.SetState(GameState.PLAYING);
        }

        public void Close()
        {
            OnClosed?.Invoke(this);
        }
    }
}
