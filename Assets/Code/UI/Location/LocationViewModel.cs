using Code.UI;

namespace Cloudy.UI
{
    public sealed class LocationViewModel : ILocationViewModel
    {
        private readonly int _id;
        private readonly WeaponsMenuPresenter _weaponsMenuPresenter;
        private readonly LocationsData _locationsData;
        public string Title { get; }

        public LocationViewModel(int id, WeaponsMenuPresenter weaponsMenuPresenter, LocationsData locationsData)
        {
            _id = id;
            _weaponsMenuPresenter = weaponsMenuPresenter;
            _locationsData = locationsData;
            Title = _locationsData.Locations[_id - 1];
        }
        
        public void ShowWeaponMenu()
        {
            _locationsData.CurrentLocation = _id;
            _weaponsMenuPresenter.Show();
        }
    }
}