namespace Code.UI
{
    public interface IWeaponUpgradeButtonModel
    {
        string Description { get; }
        
        void OnUpgradeButtonClick();
    }
}