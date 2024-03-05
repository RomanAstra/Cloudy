using Code.UI;
using UnityEngine;
using Zenject;
using YG;

namespace Cloudy.UI
{
    public sealed class Menu : MonoBehaviour
    {
        private MainMenuPresenter _mainMenuPresenter;

        [Inject]
        public void Construct(MainMenuPresenter mainMenuPresenter)
        {
            _mainMenuPresenter = mainMenuPresenter;
        }
        
        private void Start()
        {
            YandexGame.StickyAdActivity(true);
            _mainMenuPresenter.Show();
        }
        private void OnDestroy()
        {
            YandexGame.StickyAdActivity(false);
        }
    }
}