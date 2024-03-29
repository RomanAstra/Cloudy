using Cloudy;
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

            var width =  _progressImage.rectTransform.rect.width;
            for (var i = 0; i < viewModel.Weapons.Count; i++)
            {
                InitializeWeaponView(viewModel.Weapons[i], viewModel.AllStarsCount, width);
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

        private void InitializeWeaponView(OpenObjectStarsData weaponData, float allStarsCount, float width)
        {
            var view = Instantiate(_weaponViewPrefab, _progressImage.transform);
            view.Initialize(new WeaponViewModel(weaponData.Id, true));
                
            //todo уточнить или создать новый префаб 
            //view.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);
            var viewTransform = view.transform;
            viewTransform.localScale = Vector2.one * 0.25f;
                
            var x = weaponData.StarsCount / allStarsCount * width;
            viewTransform.localPosition = new Vector2(x, 100);
        }
    }
}