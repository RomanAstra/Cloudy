using Code.UI;
using Ui;
using Zenject;

namespace Cloudy
{
    public sealed class GameUIInstaller
    {
        public void Binding(DiContainer container, int upgradesCount, int percentToWin)
        {
            container.Bind<EndGameObserver>().AsSingle().NonLazy();
            container.Bind<EndGamePresenter>().AsSingle().WithArguments(percentToWin);
            container.Bind<WeaponsUpgradesPresenter>().AsSingle().WithArguments(upgradesCount);
            container.BindInterfacesTo<ViewManager>().AsSingle();
        }
    }
}