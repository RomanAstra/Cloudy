using System.Collections.Generic;
using Ui;

namespace Code.UI
{
    public interface IWeaponsUpgradesViewModel : IViewModel
    {
        public IReadOnlyList<IWeaponUpgradeButtonModel> WeaponUpgradeButtonModels { get; }
    }
}