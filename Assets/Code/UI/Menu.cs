using Code.UI;
using UnityEngine;
using Zenject;
using YG;

namespace Cloudy.UI
{
    public sealed class Menu : MonoBehaviour
    {
        private MainMenuPresenter _mainMenuPresenter;
        private StarsProgressPresenter _starsProgressPresenter;

        [Inject]
        public void Construct(MainMenuPresenter mainMenuPresenter, StarsProgressPresenter starsProgressPresenter)
        {
            _mainMenuPresenter = mainMenuPresenter;
            _starsProgressPresenter = starsProgressPresenter;
        }
        
        private void Start()
        {
            YandexGame.StickyAdActivity(true);
            _mainMenuPresenter.Show();
            _starsProgressPresenter.Show();
        }
        private void OnDestroy()
        {
            YandexGame.StickyAdActivity(false);
        }
    }
}