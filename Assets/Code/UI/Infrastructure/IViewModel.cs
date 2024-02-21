using System;

namespace Ui
{
    public interface IViewModel
    {
        event Action<IViewModel> OnClosed;

        void Close();
    }
}
