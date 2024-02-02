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
        [SerializeField] private bool _isTest;
        [SerializeField] private GameObject[] _levels;
        
        public override async void InstallBindings()
        {
            Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            Container.BindInterfacesTo<InputSystemManager>().AsCached().NonLazy();

            Container.BindInterfacesTo<WeaponSystem>().AsSingle().WithArguments(_weaponsContainer);

            if (_isTest)
            {
                foreach (var level in _levels)
                {
                    level.SetActive(false);
                }
                _levels[App.CurrentLevel - 1].SetActive(true);
                return;
            }
            
            await new LevelProvider().Load(App.CurrentLevel.ToString());
        }
    }
}
