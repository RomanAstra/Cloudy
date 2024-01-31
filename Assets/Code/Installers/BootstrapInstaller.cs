using UnityEngine;
using Utils;
using Zenject;

namespace Cloudy
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameManager _gameManager;

        [SerializeField] private Player _player;

        [SerializeField] private Weapon[] _weapons;

        [SerializeField] private CloudZoneDetectorController _detectorController;
        
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            Container.BindInterfacesTo<InputSystemManager>().AsCached().NonLazy();

            Container.BindInterfacesTo<WeaponSystem>().AsSingle().WithArguments(_weapons);
        }
    }
}
