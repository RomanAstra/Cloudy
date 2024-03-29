using Ui;
using Utils;

namespace Code.UI
{
    public sealed class StartGamePresenter : BasePresenter<StartGameView, StartGameViewModel>
    {
        private readonly IViewManager _viewManager;
        private readonly GameManager _gameManager;
        private readonly TimerPresenter _timerPresenter;

        public StartGamePresenter(IViewManager viewManager, GameManager gameManager, TimerPresenter timerPresenter) :  base(viewManager)
        {
            _viewManager = viewManager;
            _gameManager = gameManager;
            _timerPresenter = timerPresenter;
        }

        public void Show()
        {
            var model = new StartGameViewModel(_gameManager, _timerPresenter);
            _viewManager.ShowWindowOutOfStack(model);
        }
    }
}