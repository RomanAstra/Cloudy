using UnityEngine;
using Utils;
using Zenject;

namespace Cloudy
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameManager _gameManager;

        [SerializeField] private Player _player;
        
        [SerializeField] private Transform _weaponsContainer;

        [SerializeField] private CloudZoneDetectorController _detectorController;
        
        [SerializeField] private bool _isTest;
        [SerializeField] private GameObject[] _levels;

        [SerializeField] private int _openWeaponIndex;
        
        public override async void InstallBindings()
        {
            App.OpenWeaponIndex = _openWeaponIndex;
            
            Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            Container.BindInterfacesTo<InputSystemManager>().AsCached().NonLazy();

            Container.BindInterfacesTo<WeaponSystem>().AsSingle().WithArguments(_weaponsContainer);

            Container.Bind<CloudZoneDetectorController>().FromInstance(_detectorController).AsSingle();
            
            if (_isTest)
            {
                if(_levels.Length > 0)
                    Instantiate(_levels[App.CurrentLevel - 1]);
                return;
            }
            
            await new LevelProvider().Load(App.CurrentLevel.ToString());
        }
    }
}
