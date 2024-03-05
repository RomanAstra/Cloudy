namespace Cloudy.UI
{
    public interface ILocationViewModel
    {
        string Title { get; }
        string Stars { get; }
        string NeedStarsToOpen { get; }

        void ShowWeaponMenu();
    }
}