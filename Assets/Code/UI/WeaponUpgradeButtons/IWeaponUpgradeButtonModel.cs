namespace Code.UI
{
    public interface IWeaponUpgradeButtonModel
    {
        public string Description { get; }
        
        void OnUpgradeButtonClick();
    }
}