using Code;
using Zenject;

namespace Cloudy
{
    public sealed class GameInstaller
    {
        public void Binding(DiContainer container, PlayerHierarchy player, 
            CloudZoneDetectorController detectorController, int allEnemiesDamage)
        {
            container.BindInstance(player).AsSingle();
            container.BindInterfacesTo<PlayerAdapter>().AsSingle();
            container.BindInterfacesTo<InputSystemManager>().AsCached().NonLazy();
            container.Bind<AllEnemiesDamageSystem>().AsSingle().WithArguments(allEnemiesDamage);

            container.BindInstance(detectorController).AsSingle();
        }
    }
}