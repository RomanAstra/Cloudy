using Cloudy;

namespace Code.UI
{
    public interface ITimerModel
    {
        Countdown Timer { get; }
        void SetReward();
    }
}