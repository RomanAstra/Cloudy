using Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public sealed class StartGameView : MonoBehaviour, IView<IStartGameViewModel>
    {
        [SerializeField] private Button _startGameButton;
        
        private IStartGameViewModel _viewModel;
        public IViewModel ViewModel => _viewModel;

        void IView<IStartGameViewModel>.Initialize(IStartGameViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(StartGame);
        }
        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            _viewModel.StartGame();
            Hide();
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
            Hide();
        }
        public void Focus()
        {
            Show();
        }
    }
}