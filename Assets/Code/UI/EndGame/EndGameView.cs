using TMPro;
using Ui;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

namespace Code.UI
{
    public sealed class EndGameView : MonoBehaviour, IView<IEndGameViewModel>
    {
        [SerializeField] private TextMeshProUGUI _resultGameText;
        [SerializeField] private LevelProgressView _levelProgressView;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _adsButton;

        private IEndGameViewModel _viewModel;

        public IViewModel ViewModel => _viewModel;

        [Inject]
        public void Construct()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _exitButton.onClick.AddListener(ExitGame);
            _continueButton.onClick.AddListener(ContinueGame);
            _adsButton.onClick.AddListener(ShowAds);
        }
        void IView<IEndGameViewModel>.Initialize(IEndGameViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
            _adsButton.onClick.RemoveAllListeners();
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            _viewModel.Show();
            
            _resultGameText.text = _viewModel.ResultGame;
            
            _levelProgressView.Initialize(_viewModel.ProgressLevelModel);
            
            _continueButton.gameObject.SetActive(_viewModel.CanContinue);
            _adsButton.gameObject.SetActive(_viewModel.CanShowAds);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
            _viewModel.Close();
        }
        public void Unfocus()
        {
            Hide();
        }
        public void Focus()
        {
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
            _viewModel.ContinueGame();
        }
        private void ShowAds()
        {
            Hide();
            _viewModel.ShowAds();
        }
    }
}