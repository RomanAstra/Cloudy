using Cloudy;
using Ui;

namespace Code.UI
{
    public interface ITimerViewModel : IViewModel
    {
        Countdown Timer { get; }
    }
}