using System;
using Cloudy;
using Ui;
using Utils;
using YG;

namespace Code.UI
{
    public sealed class TimerViewModel : ITimerViewModel, IUpdate, IDisposable
    {
        public Countdown Timer { get; }

        public event Action<IViewModel> OnClosed;
        public event Action OnTimeOver;
        
        public TimerViewModel(float time)
        {
            Timer = new Countdown(time, time);
            YandexGame.RewardVideoEvent += SetReward;
        }

        public void OnUpdate(float deltaTime)
        {
            if(Timer.IsEnded)
                return;
            
            Timer.Update();
            
            if(!Timer.IsEnded)
                return;
            
            OnTimeOver?.Invoke();
        }
        public void Close()
        {
            OnClosed?.Invoke(this);
        }
        void IDisposable.Dispose()
        {
            YandexGame.RewardVideoEvent -= SetReward;
        }
        
        private void SetReward(int id)
        {
            Timer.CurrentTime = 15;
        }
    }
}