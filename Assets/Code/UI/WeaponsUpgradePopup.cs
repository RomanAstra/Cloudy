using Configs.Upgrades.Weapons;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;

namespace Cloudy.UI
{
    public sealed class WeaponsUpgradePopup : MonoBehaviour
    {
        [SerializeField] private WeaponButtonView _weaponButtonViewPrefab;
        [SerializeField] private Transform _viewParent;
        [SerializeField] private int _viewCount = 3;
        
        private AllWeaponsUpgradeConfig _weaponsUpgradeConfig;
        
        [Inject]
        public void Construct(AllWeaponsUpgradeConfig weaponsUpgradeConfig)
        {
            _weaponsUpgradeConfig = weaponsUpgradeConfig;
        }
        public void Show()
        {
            gameObject.SetActive(true);
            
            for (var i = 0; i < _viewCount; i++)
            {
                var weapon = App.CurrentWeapons[Random.Range(0, App.CurrentWeapons.Count)];
                var configs = _weaponsUpgradeConfig.GetConfig(weapon);
                if(configs == null)
                    continue;

                var config = configs.GetRandomConfig();

                Debug.Log(config.Type);
                if(config == null)
                    continue;

                var view = Instantiate(_weaponButtonViewPrefab, _viewParent);
                var text = $"{weapon} {config.Type} {config.Value}";
                view.Set(text, () =>
                {
                    configs.Remove(config);
                    WeaponUpgradeSystem.AddUpgrade(weapon, config.Type, config.Value);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().path);
                });
            }
        }
    }
}