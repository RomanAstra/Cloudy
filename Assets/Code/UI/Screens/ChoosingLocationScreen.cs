using Extensions;
using UnityEngine;

namespace Cloudy.UI.Screens
{
    public sealed class ChoosingLocationScreen : BaseScreen
    {
        [SerializeField] private LocationView _locationViewPrefab;
        [SerializeField] private Transform _parent;
        
        protected override void Awake()
        {
            base.Awake();

            for (var i = 0; i < App.Locations.Length; i++)
            {
                var view = Instantiate(_locationViewPrefab, _parent);
                view.SetView(App.Locations[i], i + 1);
            }
        }
    }
}