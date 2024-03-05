using System.Collections.Generic;
using Cloudy.UI;
using Ui;

namespace Code.UI
{
    public interface ILocationsMenuViewModel : IViewModel
    {
        string Title { get; }
        IReadOnlyList<ILocationViewModel> LocationViewModels { get; }
    }
}