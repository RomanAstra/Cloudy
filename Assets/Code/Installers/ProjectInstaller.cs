using Cloudy.SaveData;
using Configs.Upgrades.Weapons;
using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private AllWeaponsUpgradeConfig _weaponsUpgradeConfig; 
        [SerializeField] private OpenObjectStarsConfig _weaponsConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_weaponsUpgradeConfig);
            Container.Bind<WeaponUpgradeProvider>().AsSingle();
            Container.Bind<WeaponUpgradeSystem>().AsSingle();
            Container.Bind<WeaponsDataProvider>().AsSingle().WithArguments(_weaponsConfig.Objects);
            Container.Bind<LocationsData>().AsSingle();
            
            var saveSystem = new SaveSystem();
            Container.BindInstance(saveSystem);

            var yandexGameInstaller = new YandexGameInstaller();
            yandexGameInstaller.Binding(saveSystem);
        }
    }
}