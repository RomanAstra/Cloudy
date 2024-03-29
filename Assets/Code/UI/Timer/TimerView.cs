using TMPro;
using Ui;
using UnityEngine;

namespace Code.UI
{
    public sealed class TimerView : MonoBehaviour, IView<ITimerViewModel>
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        
        private ITimerViewModel _viewModel;
        
        public IViewModel ViewModel => _viewModel;
        
        public void Initialize(ITimerViewModel viewModel)
        {
            _viewModel = viewModel;
            SetTime();
        }

        private void LateUpdate()
        {
            SetTime();
        }

        private void SetTime()
        {
            if (_viewModel == null) 
                return;
            
            _timerText.text = _viewModel.Timer.CurrentTime.ToString("00");
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
            Show();
        }
    }
}