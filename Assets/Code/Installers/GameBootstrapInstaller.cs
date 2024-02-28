using Code.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;
using Zenject;

namespace Cloudy
{
    public sealed class GameBootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private TimerView _timer;

        [SerializeField] private PlayerHierarchy _player;

        [SerializeField] private CloudZoneDetectorController _detectorController;

        [SerializeField] private Transform _bulletsPurent;

        [SerializeField] private float _roundTime = 60f;
        [SerializeField] private int _percentToWin = 50;
        [SerializeField] private int _weaponsUpgradesCount = 3;

        public override void InstallBindings()
        {
            InstallBindingsAsync().Forget();
        }

        private async UniTaskVoid InstallBindingsAsync()
        {
            Container.BindInstance(_gameManager).AsSingle();
            
            var gameInstaller = new GameInstaller();
            gameInstaller.Binding(Container, _timer, _roundTime, _player, _detectorController);
            
            var gameUIInstaller = new GameUIInstaller();
            gameUIInstaller.Binding(Container, _weaponsUpgradesCount, _percentToWin);
            
            var weaponInstaller = new WeaponsInstaller();
            weaponInstaller.Binding(Container, _player.WeaponContainer);
            
            var cloudInstaller = new CloudsInstaller();
            cloudInstaller.Binding(Container);

            var bulletInstaller = new BulletsInstaller(); 
            bulletInstaller.Binding(Container, _bulletsPurent);
            
            Container.BindInterfacesTo<LevelModel>().AsSingle();
            var locationsData = Container.Resolve<LocationsData>();
            var level = await new LevelProvider().Load(locationsData.GetLevelName());
            level.Initialize(Container.Resolve<ILevelModel>());

            await UniTask.Delay(1000);
            
            _gameManager.SetState(GameState.PLAYING);
        }
    }
}
