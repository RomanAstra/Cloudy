using TMPro;
using Ui;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI
{
    public sealed class EndGameView : MonoBehaviour, IView<IEndGameViewModel>
    {
        [SerializeField] private TextMeshProUGUI _resultGameText;
        [SerializeField] private OpenWeaponView _openedWeaponView;
        [SerializeField] private LevelProgressView _levelProgressView;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _continurButton;

        private IEndGameViewModel _viewModel;

        public IViewModel ViewModel => _viewModel;

        [Inject]
        public void Construct()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _exitButton.onClick.AddListener(ExitGame);
            _continurButton.onClick.AddListener(ContinueGame);
        }
        void IView<IEndGameViewModel>.Initialize(IEndGameViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _continurButton.onClick.RemoveAllListeners();
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            _viewModel.Show();
            
            _resultGameText.text = _viewModel.ResultGame;
            
            _openedWeaponView.Initialize(_viewModel.OpenWeaponModel);
            
            _levelProgressView.Initialize(_viewModel.ProgressLevelModel);
            
            _continurButton.gameObject.SetActive(_viewModel.CanContinue);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public void Unfocus()
        {
            Hide();
        }
        public void Focus()
        {
            Show();
        }
        
        private void RestartGame()
        {
            _viewModel.RestartGame();
            
        }
        private void ExitGame()
        {
            _viewModel.ExitGame();
        }
        private void ContinueGame()
        {
            _viewModel.ShowWeaponsUpgrades();
        }
    }
}