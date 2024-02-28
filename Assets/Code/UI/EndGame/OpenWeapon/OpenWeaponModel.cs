namespace Code.UI
{
    public sealed class OpenWeaponModel : IOpenWeaponModel
    {
        public string OpenWeapon { get; }

        public OpenWeaponModel(string openWeapon)
        {
            OpenWeapon = openWeapon;
        }
    }
}