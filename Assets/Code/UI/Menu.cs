using Cloudy.UI.Screens;
using Configs.Upgrades.Weapons;
using Extensions;
using UnityEngine;
using Zenject;

namespace Cloudy.UI
{
    public class Menu : MonoBehaviour
    {
        private WeaponUpgradeProvider _weaponUpgradeProvider;
        private WeaponUpgradeSystem _weaponUpgradeSystem;
        
        [Inject]
        public void Construct(WeaponUpgradeProvider weaponUpgradeProvider, WeaponUpgradeSystem weaponUpgradeSystem)
        {
            _weaponUpgradeProvider = weaponUpgradeProvider;
            _weaponUpgradeSystem = weaponUpgradeSystem;
        }
        
        private void Start()
        {
            _weaponUpgradeProvider.Reset();
            _weaponUpgradeSystem.Reset();
            ScreenManager.Show<MainScreen>();
        }
    }
}