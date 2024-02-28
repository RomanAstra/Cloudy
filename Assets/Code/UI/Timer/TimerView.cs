using System;
using TMPro;
using UnityEngine;
using Utils;

namespace Code.UI
{
    public sealed class TimerView : MonoBehaviour, IUpdate
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        
        private ITimerModel _viewModel;

        public event Action TimeOver;
        
        public void Initialize(ITimerModel viewModel)
        {
            _viewModel = viewModel;
            SetTime();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if(_viewModel.Timer.IsEnded)
                return;
            
            _viewModel.Timer.Update();

            SetTime();
            
            if(!_viewModel.Timer.IsEnded)
                return;
            
            TimeOver?.Invoke();
        }

        private void SetTime()
        {
            _timerText.text = _viewModel.Timer.CurrentTime.ToString("00");
        }
    }
}