using Ui;

namespace Code.UI
{
    public interface IEndGameViewModel : IViewModel
    {
        ILevelProgressModel ProgressLevelModel { get; }
        string ResultGame { get; }
        bool CanContinue { get; }
        bool CanShowAds { get; }

        void Show();
        void ContinueGame();
        void RestartGame();
        void ExitGame();
        void ShowAds();
        void GameResume();
    }
}