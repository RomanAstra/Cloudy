using Ui;
using Utils;
using YG;

namespace Code.UI
{
    public sealed class StartGameTimerPresenter : BasePresenter<StartGameTimerView, StartGameTimerViewModel>
    {
        private const float TIME_TO_START_GAME = 3f;
            
        private readonly IViewManager _viewManager;
        private readonly GameManager _gameManager;
        private readonly AllEnemiesDamageSystem _allEnemiesDamageSystem;

        public StartGameTimerPresenter(IViewManager viewManager, GameManager gameManager, 
            AllEnemiesDamageSystem allEnemiesDamageSystem) : base(viewManager)
        {
            _viewManager = viewManager;
            _gameManager = gameManager;
            _allEnemiesDamageSystem = allEnemiesDamageSystem;

            YandexGame.CloseVideoEvent += Show;
        }
        
        private void Show()
        {
            YandexGame.CloseVideoEvent -= Show;
            
            var model = new StartGameTimerViewModel(_gameManager, _allEnemiesDamageSystem,TIME_TO_START_GAME);
            _viewManager.ShowWindow(model);
        }
    }
}