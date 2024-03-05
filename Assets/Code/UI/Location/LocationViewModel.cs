using Cloudy.SaveData;
using Code.UI;

namespace Cloudy.UI
{
    public sealed class LocationViewModel : ILocationViewModel
    {
        private readonly WeaponsMenuPresenter _weaponsMenuPresenter;
        private readonly LocationsData _locationsData;
        public string Title { get; }
        public string Stars { get; } = "0/15";
        public string NeedStarsToOpen { get; }

        public LocationViewModel(OpenObjectStarsData location, WeaponsMenuPresenter weaponsMenuPresenter, LocationsData locationsData, 
            SaveSystem saveSystem)
        {
            Title = location.Id;
            _weaponsMenuPresenter = weaponsMenuPresenter;
            _locationsData = locationsData;

            if(saveSystem.SaveData.TryGetLocationData(Title, out var data))
                Stars = $"{data.Stars}/15";

            var starsCount = saveSystem.SaveData.GetStarsCount();
            if(location.StarsCount > starsCount)
                NeedStarsToOpen = $"{starsCount}/{location.StarsCount}";
        }
        
        public void ShowWeaponMenu()
        {
            _locationsData.CurrentLocation = Title;
            _weaponsMenuPresenter.Show();
        }
    }
}