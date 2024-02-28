using System;
using Ui;

namespace Code.UI
{
    public sealed class WeaponsMenuViewModel : IWeaponsMenuViewModel
    {
        public string Title => "Оружие";
        
        public event Action<IViewModel> OnClosed;

        public void Close()
        {
            OnClosed?.Invoke(this);
        }
    }
}