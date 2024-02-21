using TMPro;
using Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public sealed class MainMenuView : MonoBehaviour, IView<IMainMenuViewModel>
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingsButton;
        
        private IMainMenuViewModel _viewModel;
        public IViewModel ViewModel => _viewModel;

        public void Initialize(IMainMenuViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(StartGame);
            _settingsButton.onClick.AddListener(ShowSettings);
        }
        
        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(StartGame);
            _settingsButton.onClick.RemoveListener(ShowSettings);
        }

        private void ShowSettings()
        {
            _viewModel.ShowSettings();
        }

        private void StartGame()
        {
            _viewModel.StartGame();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Unfocus()
        {
        }

        public void Focus()
        {
        }
    }
}