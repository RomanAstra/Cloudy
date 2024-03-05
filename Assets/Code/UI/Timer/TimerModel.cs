using Cloudy;

namespace Code.UI
{
    public sealed class TimerModel : ITimerModel
    {
        public Countdown Timer { get; }
        
        public TimerModel(float time)
        {
            Timer = new Countdown(time, time);
        }
        
        public void SetReward()
        {
            Timer.CurrentTime = 15;
        }
    }
}