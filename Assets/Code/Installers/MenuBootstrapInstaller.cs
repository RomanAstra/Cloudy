using Code.UI;
using Ui;
using Zenject;

namespace Cloudy
{
    public sealed class MenuBootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainMenuPresenter>().AsSingle();
            Container.Bind<LocationsMenuPresenter>().AsSingle();
            Container.Bind<WeaponsMenuPresenter>().AsSingle();
            Container.BindInterfacesTo<ViewManager>().AsSingle();
        }
    }
}