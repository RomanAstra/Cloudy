using Ui;

namespace Code.UI
{
    public interface IEndGameViewModel : IViewModel
    {
        public IOpenWeaponModel OpenWeaponModel { get; }
        public ILevelProgressModel ProgressLevelModel { get; }
        public string ResultGame { get; }
        public bool CanContinue { get; }
        
        void Show();
        void ShowWeaponsUpgrades();
        void RestartGame();
        void ExitGame();
    }
}