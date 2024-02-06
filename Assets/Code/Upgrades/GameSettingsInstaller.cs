using UnityEngine;
using Zenject;

namespace Configs.Upgrades.Weapons
{
    [CreateAssetMenu(menuName = "Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private AllWeaponsUpgradeConfig _weaponsUpgradeConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_weaponsUpgradeConfig);
        }
    }
}