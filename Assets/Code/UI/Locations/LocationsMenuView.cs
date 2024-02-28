using System;
using Cloudy.UI;
using TMPro;
using Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public sealed class LocationsMenuView : MonoBehaviour, IView<ILocationsMenuViewModel>
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private LocationView _locationViewPrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private Button _backButton;
        
        private ILocationsMenuViewModel _viewModel;
        public IViewModel ViewModel => _viewModel;

        void IView<ILocationsMenuViewModel>.Initialize(ILocationsMenuViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        
        private void Awake()
        {
            _titleText.text = _viewModel.Title;
            
            for (var i = 0; i < _viewModel.LocationViewModels.Count; i++)
            {
                var view = Instantiate(_locationViewPrefab, _parent);
                view.Initialize(_viewModel.LocationViewModels[i]);
            }
        }
        private void OnEnable()
        {
            _backButton.onClick.AddListener(_viewModel.Close);
        }
        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(_viewModel.Close);
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