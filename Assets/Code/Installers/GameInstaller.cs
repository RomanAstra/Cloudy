using Code.UI;
using Zenject;

namespace Cloudy
{
    public sealed class GameInstaller
    {
        public void Binding(DiContainer container, TimerView timer, float roundTime, PlayerHierarchy player, 
            CloudZoneDetectorController detectorController)
        {
            timer.Initialize(new TimerModel(roundTime));
            container.BindInterfacesAndSelfTo<TimerView>().FromInstance(timer).AsSingle();
            
            container.BindInstance(player).AsSingle();
            container.BindInterfacesTo<PlayerAdapter>().AsSingle();
            container.BindInterfacesTo<InputSystemManager>().AsCached().NonLazy();

            container.BindInstance(detectorController).AsSingle();
        }
    }
}