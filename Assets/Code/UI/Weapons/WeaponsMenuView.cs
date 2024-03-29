using System.Collections.Generic;
using Cloudy.UI;
using TMPro;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Code.UI
{
    public sealed class WeaponsMenuView : MonoBehaviour, IView<IWeaponsMenuViewModel>
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private WeaponView _weaponViewPrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private Button _rollWeaponsButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _backButton;

        private IWeaponsMenuViewModel _viewModel;
        private Pool<WeaponView> _pool;
        private readonly List<WeaponView> _weaponViews = new ();

        public IViewModel ViewModel => _viewModel;
        
        void IView<IWeaponsMenuViewModel>.Initialize(IWeaponsMenuViewModel viewModel)
        {
            _viewModel = viewModel;
            _titleText.text = viewModel.Title;

            _pool = new Pool<WeaponView>(_weaponViewPrefab);
            
            RollWeapons();
        }
        
        private void OnEnable()
        {
            _rollWeaponsButton.onClick.AddListener(RollWeapons);
            _playButton.onClick.AddListener(_viewModel.LoadScene);
            _backButton.onClick.AddListener(_viewModel.Close);
        }
        private void OnDisable()
        {
            _rollWeaponsButton.onClick.RemoveListener(RollWeapons);
            _playButton.onClick.RemoveListener(_viewModel.LoadScene);
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
        private void RollWeapons()
        {
            for (var i = 0; i < _weaponViews.Count; i++)
            {
                _pool.Release(_weaponViews[i]);
            }

            _weaponViews.Clear();

            _viewModel.RollWeapons();
            foreach (var weapon in _viewModel.Weapons)
            {
                var view = _pool.Get(Vector3.zero, Quaternion.identity, _parent);
                _weaponViews.Add(view);
                view.Initialize(new WeaponViewModel(weapon, false));
            }
        }
    }
}