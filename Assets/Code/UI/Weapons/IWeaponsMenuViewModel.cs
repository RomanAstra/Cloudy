using System.Collections.Generic;
using Ui;

namespace Code.UI
{
    public interface IWeaponsMenuViewModel : IViewModel
    {
        string Title { get; }
        IReadOnlyList<string> Weapons { get; }
        void LoadScene();
        void RollWeapons();
    }
}