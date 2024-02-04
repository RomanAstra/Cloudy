using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Cloudy.UI.Screens
{
    public sealed class WeaponsScreen : BaseScreen
    {
        [SerializeField] private WeaponView _weaponViewPrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private int _weaponsCount = 3;
        [SerializeField] private Button _rollWeaponsButton;
        [SerializeField] private Button _playButton;

        private Pool<WeaponView> _pool;
        private readonly List<WeaponView> _weaponViews = new ();

        protected override void Awake()
        {
            base.Awake();
            
            _rollWeaponsButton.onClick.AddListener(RollWeapons);
            _playButton.onClick.AddListener(() => SceneManager.LoadScene(App.CurrentLocation));

            _pool = new Pool<WeaponView>(_weaponViewPrefab);
            
            RollWeapons();
        }

        private void RollWeapons()
        {
            foreach (var weaponView in _weaponViews)
            {
                _pool.Release(weaponView);
            }
            
            App.CurrentWeapons.Clear();
            App.CurrentWeapons.AddRange(App.Weapons.Take(App.OpenWeaponIndex + 1).OrderBy(w => Random.value).Take(_weaponsCount));
            
            foreach (var weapon in App.CurrentWeapons)
            {
                var view = _pool.Get(Vector3.zero, Quaternion.identity, _parent);
                _weaponViews.Add(view);
                view.SetView(weapon);
            }
        }
    }
}