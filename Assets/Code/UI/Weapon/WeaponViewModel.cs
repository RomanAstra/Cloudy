namespace Cloudy.UI
{
    public sealed class WeaponViewModel : IWeaponViewModel
    {
        public string WeaponName { get; }
        public bool WithIndicator { get; }

        public WeaponViewModel(string weaponName, bool withIndicator)
        {
            WeaponName = weaponName;
            WithIndicator = withIndicator;
        }
    }
}