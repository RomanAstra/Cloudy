using System;
using System.Collections.Generic;
using Cloudy;
using Cloudy.UI;
using Ui;

namespace Code.UI
{
    public sealed class LocationsMenuViewModel : ILocationsMenuViewModel
    {
        private readonly WeaponsMenuPresenter _weaponsMenuPresenter;
        private readonly LocationsData _locationsData;
        public string Title => "Выбор локации";
        public IReadOnlyList<ILocationViewModel> LocationViewModels { get; }

        public event Action<IViewModel> OnClosed;

        public LocationsMenuViewModel(WeaponsMenuPresenter weaponsMenuPresenter, LocationsData locationsData)
        {
            _weaponsMenuPresenter = weaponsMenuPresenter;
            _locationsData = locationsData;

            var length = _locationsData.Locations.Length;
            var models = new List<ILocationViewModel>(length);

            for (var i = 0; i < length; i++)
            {
                models.Add(new LocationViewModel(i + 1, weaponsMenuPresenter, _locationsData));
            }
            
            LocationViewModels = models;
        }
        
        public void ShowWeaponsMenu()
        {
            _weaponsMenuPresenter.Show();
        }
        public void Close()
        {
            OnClosed?.Invoke(this);
        }
    }
}