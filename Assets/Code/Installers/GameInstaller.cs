using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class GameInstaller
    {
        public void Binding(DiContainer container, Timer timer, PlayerHierarchy player, 
            CloudZoneDetectorController detectorController, LevelHierarchy level)
        {
            container.Bind<LevelHierarchy>().FromInstance(level).AsSingle().NonLazy();
            container.BindInterfacesTo<Timer>().FromInstance(timer).AsSingle();
            
            container.Bind<PlayerHierarchy>().FromInstance(player).AsSingle();
            container.BindInterfacesTo<PlayerAdapter>().AsSingle().NonLazy();
            container.BindInterfacesTo<InputSystemManager>().AsCached().NonLazy();

            container.Bind<CloudZoneDetectorController>().FromInstance(detectorController).AsSingle();
        }
    }
}