using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;
using Zenject;

namespace Cloudy
{
    public sealed class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Timer _timer;

        [SerializeField] private Player _player;

        [SerializeField] private CloudZoneDetectorController _detectorController;

        [SerializeField] private Transform _bulletsPurent;

        public override void InstallBindings()
        {
            InstallBindingsAsync().Forget();
        }

        private async UniTaskVoid InstallBindingsAsync()
        {
            var preload = FindObjectOfType<PreloadAssets>();

            Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
            
            var gameInstaller = new GameInstaller();
            gameInstaller.Binding(Container, _timer, _player, _detectorController, preload.Level);
            
            var weaponInstaller = new WeaponsInstaller();
            weaponInstaller.Binding(Container, _player.WeaponContainer, preload.Weapons);
            
            var cloudInstaller = new CloudsInstaller();
            cloudInstaller.Binding(Container);

            var bulletInstaller = new BulletsInstaller(); 
            bulletInstaller.Binding(Container, _bulletsPurent);
            
            await UniTask.Delay(1000);
            
            _gameManager.SetState(GameState.PLAYING);
        }
    }
}
