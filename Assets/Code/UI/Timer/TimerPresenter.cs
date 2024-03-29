using Ui;
using Utils;

namespace Code.UI
{
    public sealed class TimerPresenter : BasePresenter<TimerView, TimerViewModel>
    {
        private readonly IViewManager _viewManager;
        private readonly GameManager _gameManager;
        private readonly TimerViewModel _model;

        public TimerPresenter(IViewManager viewManager, GameManager gameManager, TimerViewModel model) : base(viewManager)
        {
            _viewManager = viewManager;
            _gameManager = gameManager;
            _model = model;
        }
        
        public void Show()
        {
            _gameManager.Add(_model);
            _viewManager.ShowWindowOutOfStack(_model);
        }
    }
}