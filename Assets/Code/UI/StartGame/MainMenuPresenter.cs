using Ui;
using Utils;

namespace Code.UI
{
    public sealed class StartGamePresenter : BasePresenter<StartGameView, StartGameViewModel>
    {
        private readonly IViewManager _viewManager;
        private readonly GameManager _gameManager;

        public StartGamePresenter(IViewManager viewManager, GameManager gameManager) :  base(viewManager)
        {
            _viewManager = viewManager;
            _gameManager = gameManager;
        }

        public override void Show()
        {
            var model = new StartGameViewModel(_gameManager);
            _viewManager.ShowWindowOutOfStack(model);
        }
    }
}