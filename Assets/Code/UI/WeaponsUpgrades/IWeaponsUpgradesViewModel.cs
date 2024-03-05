using System.Collections.Generic;
using Ui;

namespace Code.UI
{
    public interface IWeaponsUpgradesViewModel : IViewModel
    {
        IReadOnlyList<IWeaponUpgradeButtonModel> WeaponUpgradeButtonModels { get; }
    }
}