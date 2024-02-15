using System.Text;
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
        
        private WeaponUpgradeProvider _weaponUpgradeProvider;
        private WeaponUpgradeSystem _weaponUpgradeSystem;
        
        [Inject]
        public void Construct(WeaponUpgradeProvider weaponUpgradeProvider, WeaponUpgradeSystem weaponUpgradeSystem)
        {
            _weaponUpgradeProvider = weaponUpgradeProvider;
            _weaponUpgradeSystem = weaponUpgradeSystem;
        }
        public void Show()
        {
            gameObject.SetActive(true);
            
            for (var i = 0; i < _viewCount; i++)
            {
                var weapon = App.CurrentWeapons[Random.Range(0, App.CurrentWeapons.Count)];
                var config = _weaponUpgradeProvider.GetConfig(weapon);

                //todo for test
                if(config == null)
                    continue;
                
                var view = Instantiate(_weaponButtonViewPrefab, _viewParent);
                var sb = new StringBuilder(weapon);
                
                for (var j = 0; j < config.Stats.Length; j++)
                {
                    var stat = config.Stats[j];
                    sb.Append($"\n{stat.Type} = {stat.Value}");
                }
                
                view.Set(sb.ToString(), () =>
                {
                    _weaponUpgradeProvider.RemoveConfig(config);

                    for (var i = 0; i < config.Stats.Length; i++)
                    {
                        var stat = config.Stats[i];
                        _weaponUpgradeSystem.AddUpgrade(weapon, stat.Type, stat.Value);
                    }

                    SceneManager.LoadScene(SceneManager.GetActiveScene().path);
                });
            }
        }
    }
}