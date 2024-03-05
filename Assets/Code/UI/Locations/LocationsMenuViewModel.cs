using System;
using System.Collections.Generic;
using Cloudy;
using Cloudy.SaveData;
using Cloudy.UI;
using Ui;
using LocationsData = Cloudy.LocationsData;

namespace Code.UI
{
    public sealed class LocationsMenuViewModel : ILocationsMenuViewModel
    {
        private readonly LocationsData _locationsData;
        public string Title => "Выбор локации";
        public IReadOnlyList<ILocationViewModel> LocationViewModels { get; }

        public event Action<IViewModel> OnClosed;

        public LocationsMenuViewModel(WeaponsMenuPresenter weaponsMenuPresenter, LocationsData locationsData, 
            OpenObjectStarsConfig openObjectStarsConfig, SaveSystem saveSystem)
        {
            _locationsData = locationsData;

            var length = openObjectStarsConfig.Objects.Count;
            var models = new List<ILocationViewModel>(length);

            for (var i = 0; i < length; i++)
            {
                var location = openObjectStarsConfig.Objects[i];
                models.Add(new LocationViewModel(location, weaponsMenuPresenter, _locationsData, saveSystem));
            }
            
            LocationViewModels = models;
        }
        
        public void Close()
        {
            OnClosed?.Invoke(this);
        }
    }
}