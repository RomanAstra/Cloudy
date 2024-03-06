using System.Collections.Generic;
using Cloudy;
using Ui;

namespace Code.UI
{
    public interface IStarsProgressViewModel : IViewModel
    {
        float Progress { get; }
        string CurrentStarsCount { get; }
        int AllStarsCount { get; }
        IReadOnlyList<OpenObjectStarsData> Weapons { get; }
    }
}