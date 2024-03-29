using Cloudy;
using Ui;

namespace Code.UI
{
    public interface IStartGameTimerViewModel : IViewModel
    {   
        Countdown Timer { get; }
        void OnUpdate();
    }
}