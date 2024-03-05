using Code.UI;
using Ui;
using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class MenuBootstrapInstaller : MonoInstaller
    {
        [SerializeField] private OpenObjectStarsConfig _locationsStarsConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<MainMenuPresenter>().AsSingle();
            Container.Bind<LocationsMenuPresenter>().AsSingle().WithArguments(_locationsStarsConfig);
            Container.Bind<WeaponsMenuPresenter>().AsSingle();
            Container.BindInterfacesTo<ViewManager>().AsSingle();
        }
    }
}