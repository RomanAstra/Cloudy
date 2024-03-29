using System;
using TMPro;
using Ui;
using UnityEngine;

namespace Code.UI
{
    public sealed class StartGameTimerView : MonoBehaviour, IView<IStartGameTimerViewModel>
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        
        private IStartGameTimerViewModel _viewModel;
        
        public IViewModel ViewModel => _viewModel;
        
        public void Initialize(IStartGameTimerViewModel viewModel)
        {
            _viewModel = viewModel;
            
            SetTime();
        }

        private void Update()
        {
            _viewModel.OnUpdate();
        }
        private void LateUpdate()
        {
            SetTime();
        }

        private void SetTime()
        {
            if (_viewModel == null) 
                return;
            
            _timerText.text = _viewModel.Timer.CurrentTime.ToString("0");
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