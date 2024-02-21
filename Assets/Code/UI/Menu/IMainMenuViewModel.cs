using Ui;

namespace Code.UI
{
    public interface IMainMenuViewModel : IViewModel
    {
        string Title { get; }
        void StartGame();
        void ShowSettings();
    }
}
