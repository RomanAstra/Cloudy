using Code.UI;
using Ui;
using Zenject;

namespace Cloudy
{
    public sealed class GameUIInstaller
    {
        public void Binding(DiContainer container, int upgradesCount, float roundTime)
        {
            container.Bind<EndGameObserver>().AsSingle().NonLazy();

            var model = new TimerViewModel(roundTime);
            container.BindInterfacesAndSelfTo<TimerViewModel>().FromInstance(model).AsSingle().WithArguments(roundTime);
            
            container.Bind<EndGamePresenter>().AsSingle();
            container.Bind<WeaponsUpgradesPresenter>().AsSingle().WithArguments(upgradesCount);
            container.Bind<TimerPresenter>().AsSingle();
            container.Bind<StartGameTimerPresenter>().AsSingle().NonLazy();
            
            container.BindInterfacesTo<ViewManager>().AsSingle();
        }
    }
}