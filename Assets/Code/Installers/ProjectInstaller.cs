using Configs.Upgrades.Weapons;
using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private AllWeaponsUpgradeConfig _weaponsUpgradeConfig; 
        [SerializeField] private WeaponsConfig _weaponsConfig; 

        public override void InstallBindings()
        {
            Container.BindInstance(_weaponsUpgradeConfig);
            Container.Bind<WeaponUpgradeProvider>().AsSingle();
            Container.Bind<WeaponUpgradeSystem>().AsSingle();
            Container.Bind<OpenWeaponsPrefsController>().AsSingle();
            Container.Bind<WeaponsDataProvider>().AsSingle().WithArguments(_weaponsConfig.Weapons);
            Container.Bind<LocationsData>().AsSingle();
        }
    }
}