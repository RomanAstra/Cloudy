using Code.UI;
using UnityEngine;
using Zenject;

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
            _mainMenuPresenter.Show();
        }
    }
}