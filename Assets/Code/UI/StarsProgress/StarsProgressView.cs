using Cloudy.UI;
using TMPro;
using Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public sealed class StarsProgressView : MonoBehaviour, IView<IStarsProgressViewModel>
    {
        [SerializeField] private Image _progressImage;
        [SerializeField] private TextMeshProUGUI _currentStarsCountText;
        [SerializeField] private TextMeshProUGUI _allStarsCountText;
        [SerializeField] private WeaponView _weaponViewPrefab;

        private IStarsProgressViewModel _viewModel;
        public IViewModel ViewModel => _viewModel;

        void IView<IStarsProgressViewModel>.Initialize(IStarsProgressViewModel viewModel)
        {
            _viewModel = viewModel;
            
            _progressImage.fillAmount = viewModel.Progress;
            _currentStarsCountText.text = viewModel.CurrentStarsCount;
            _allStarsCountText.text = viewModel.AllStarsCount.ToString();

            for (var i = 0; i < viewModel.Weapons.Count; i++)
            {
                var weapon = viewModel.Weapons[i];
                var view = Instantiate(_weaponViewPrefab, _progressImage.transform);
                view.Initialize(weapon.Id);
                
                //todo уточнить или создать новый префаб 
                //view.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);
                view.transform.localScale = Vector2.one * 0.25f;
                
                var x = weapon.StarsCount / (float)viewModel.AllStarsCount * _progressImage.rectTransform.rect.width;
                view.transform.localPosition = new Vector2(x, 100);
            }
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
            Hide();
        }
        public void Focus()
        {
            Show();
        }
    }
}