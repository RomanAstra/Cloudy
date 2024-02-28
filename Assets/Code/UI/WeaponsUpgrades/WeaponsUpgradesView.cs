using Ui;
using UnityEngine;

namespace Code.UI
{
    public sealed class WeaponsUpgradesView : MonoBehaviour, IView<IWeaponsUpgradesViewModel>
    {
        [SerializeField] private WeaponUpgradeButtonView _weaponButtonViewPrefab;
        [SerializeField] private Transform _viewParent;

        private IWeaponsUpgradesViewModel _viewModel;

        public IViewModel ViewModel => _viewModel;

        void IView<IWeaponsUpgradesViewModel>.Initialize(IWeaponsUpgradesViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            
            for (var i = 0; i < _viewModel.WeaponUpgradeButtonModels.Count; i++)
            {
                var model = _viewModel.WeaponUpgradeButtonModels[i];
                var view = Instantiate(_weaponButtonViewPrefab, _viewParent);
                
                view.Initialize(model);
            }
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public void Unfocus()
        {
            
        }
        public void Focus()
        {
            
        }
    }
}