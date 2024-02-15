using Configs.Upgrades.Weapons;
using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class WeaponUpgradesInstaller : MonoInstaller
    {
        [SerializeField] private AllWeaponsUpgradeConfig _weaponsUpgradeConfig; 

        public override void InstallBindings()
        {
            Container.BindInstance(_weaponsUpgradeConfig);
            Container.Bind<WeaponUpgradeProvider>().AsSingle();
            Container.Bind<WeaponUpgradeSystem>().AsSingle();
        }
    }
}