using System;
using Cloudy;
using Ui;
using Utils;

namespace Code.UI
{
    public sealed class StartGameTimerViewModel : IStartGameTimerViewModel
    {
        private readonly GameManager _gameManager;
        private readonly AllEnemiesDamageSystem _allEnemiesDamageSystem;

        public Countdown Timer { get; }

        public event Action<IViewModel> OnClosed;

        public StartGameTimerViewModel(GameManager gameManager, AllEnemiesDamageSystem allEnemiesDamageSystem, float time)
        {
            _gameManager = gameManager;
            _allEnemiesDamageSystem = allEnemiesDamageSystem;
            
            Timer = new Countdown(time, time);
        }

        public void OnUpdate()
        {
            if (Timer.IsEnded)
                return;

            Timer.Update();

            if (!Timer.IsEnded)
                return;
            
            Close();
            _gameManager.SetState(GameState.PLAYING);
            _allEnemiesDamageSystem.AllEnemiesDamage();
        }
        public void Close()
        {
            OnClosed?.Invoke(this);
        }
        
    }
}