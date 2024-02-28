using System;
using UnityEngine.SceneManagement;

namespace Code.UI
{
    public sealed class WeaponUpgradeButtonModel : IWeaponUpgradeButtonModel
    {
        private readonly Action _onClickAction;
        
        public string Description { get; }

        public WeaponUpgradeButtonModel(string description, Action onClickAction)
        {
            _onClickAction = onClickAction;
            Description = description;
            
            // _weaponUpgradeProvider.RemoveConfig(config);
            //
            // for (var i = 0; i < config.Stats.Length; i++)
            // {
            //     var stat = config.Stats[i];
            //     _weaponUpgradeSystem.AddUpgrade(weapon, stat.Type, stat.Value);
            // }
            //
            // SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
        
        public void OnUpgradeButtonClick()
        {
            _onClickAction?.Invoke();
        }
    }
}