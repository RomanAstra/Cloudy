using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
    public abstract class BaseScreen : MonoBehaviour
    {
        [SerializeField]
        protected Button _showScreenButton;
        [SerializeField]
        protected BaseScreen _screenToShow;

        protected virtual void Awake()
        {
            if(_showScreenButton != null && _screenToShow != null)
                _showScreenButton.onClick.AddListener(() => ScreenManager.Show(_screenToShow));
        }

        public virtual float Show(object data)
        {
            gameObject.SetActive(true);
            return 0;
        }
        public virtual float Hide()
        {
            gameObject.SetActive(false);
            return 0;
        }
    }
}