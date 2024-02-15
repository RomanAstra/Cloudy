using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class GameInstaller
    {
        public void Binding(DiContainer container, Timer timer, Player player, 
            CloudZoneDetectorController detectorController, LevelHierarchy level)
        {
            container.Bind<LevelHierarchy>().FromInstance(level).AsSingle().NonLazy();
            container.BindInterfacesTo<Timer>().FromInstance(timer).AsSingle();
            
            container.BindInterfacesTo<Player>().FromInstance(player).AsSingle();
            container.BindInterfacesTo<InputSystemManager>().AsCached().NonLazy();

            container.Bind<CloudZoneDetectorController>().FromInstance(detectorController).AsSingle();
        }
    }
}