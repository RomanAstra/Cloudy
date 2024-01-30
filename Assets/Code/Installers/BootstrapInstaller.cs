using UnityEngine;
using Utils;
using Zenject;

namespace Cloudy
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameManager _gameManager;

        [SerializeField] private Player _player;
        
        [SerializeField] private PistolBullet _pistolBulletPrefab;
        [SerializeField] private ShotgunBullet _shotgunBulletPrefab;
        [SerializeField] private Transform _bulletsContainer;

        [SerializeField] private Weapon[] _weapons;

        [SerializeField] private CloudZoneDetectorController _detectorController;
        
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            Container.BindInterfacesTo<InputSystemManager>().AsCached().NonLazy();
            
            Container.Bind<PistolBulletPool>().AsSingle().WithArguments(_pistolBulletPrefab);
            Container.Bind<PistolBulletSpawner>().AsSingle().WithArguments(_bulletsContainer);
            
            Container.Bind<ShotgunBulletPool>().AsSingle().WithArguments(_shotgunBulletPrefab);
            Container.Bind<ShotgunBulletSpawner>().AsSingle().WithArguments(_bulletsContainer);

            Container.BindInterfacesTo<WeaponSystem>().AsSingle().WithArguments(_weapons);
        }
    }
}
