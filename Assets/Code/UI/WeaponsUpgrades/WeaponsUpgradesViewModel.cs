using System;
using System.Collections.Generic;
using System.Text;
using Cloudy;
using Cloudy.Configs;
using Configs.Upgrades.Weapons;
using Ui;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Code.UI
{
    public sealed class WeaponsUpgradesViewModel : IWeaponsUpgradesViewModel
    {
        private readonly WeaponUpgradeProvider _weaponUpgradeProvider;
        private readonly WeaponUpgradeSystem _weaponUpgradeSystem;
        public event Action<IViewModel> OnClosed;

        public IReadOnlyList<IWeaponUpgradeButtonModel> WeaponUpgradeButtonModels { get; }

        public WeaponsUpgradesViewModel(int upgradesCount, WeaponsDataProvider dataWeaponsProvider, 
            WeaponUpgradeProvider weaponUpgradeProvider, WeaponUpgradeSystem weaponUpgradeSystem)
        {
            _weaponUpgradeProvider = weaponUpgradeProvider;
            _weaponUpgradeSystem = weaponUpgradeSystem;

            var models = new List<WeaponUpgradeButtonModel>();
            
            var weapons = dataWeaponsProvider.GetCurrentWeapons();
            for (var i = 0; i < upgradesCount; i++)
            {
                var weapon = weapons[Random.Range(0, weapons.Length)];
                var config = _weaponUpgradeProvider.GetConfig(weapon);

                 var sb = new StringBuilder(weapon);
                
                 for (var j = 0; j < config.Stats.Length; j++)
                 {
                     var stat = config.Stats[j];
                     sb.Append($"\n{stat.Type} = {stat.Value}");
                 }
                 
                 models.Add(new WeaponUpgradeButtonModel(sb.ToString(), () => OnClick(weapon, config)));
            }

            WeaponUpgradeButtonModels = models;
        }
        
        public void Close()
        {
            OnClosed?.Invoke(this);
        }

        private void OnClick(string weapon, StatConfig config)
        {
            _weaponUpgradeProvider.RemoveConfig(config);

            for (var i = 0; i < config.Stats.Length; i++)
            {
                var stat = config.Stats[i];
                _weaponUpgradeSystem.AddUpgrade(weapon, stat.Type, stat.Value);
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
    }
}