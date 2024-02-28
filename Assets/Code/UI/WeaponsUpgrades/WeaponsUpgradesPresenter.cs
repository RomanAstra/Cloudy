using Cloudy;
using Configs.Upgrades.Weapons;
using Ui;

namespace Code.UI
{
    public sealed class WeaponsUpgradesPresenter : BasePresenter<WeaponsUpgradesView, WeaponsUpgradesViewModel>
    {
        private readonly IViewManager _viewManager;
        private readonly int _upgradesCount;
        private readonly WeaponsDataProvider _dataWeaponsProvider;
        private readonly WeaponUpgradeProvider _weaponUpgradeProvider;
        private readonly WeaponUpgradeSystem _weaponUpgradeSystem;

        public WeaponsUpgradesPresenter(IViewManager viewManager, int upgradesCount, 
            WeaponsDataProvider dataWeaponsProvider, WeaponUpgradeProvider weaponUpgradeProvider,
            WeaponUpgradeSystem weaponUpgradeSystem) : base(viewManager)
        {
            _viewManager = viewManager;
            _upgradesCount = upgradesCount;
            _dataWeaponsProvider = dataWeaponsProvider;
            _weaponUpgradeProvider = weaponUpgradeProvider;
            _weaponUpgradeSystem = weaponUpgradeSystem;
        }

        public override void Show()
        {
            var model = new WeaponsUpgradesViewModel(_upgradesCount, _dataWeaponsProvider, _weaponUpgradeProvider, 
                _weaponUpgradeSystem);
            _viewManager.ShowWindow(model);
        }
    }
}