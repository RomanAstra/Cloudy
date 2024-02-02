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
            
            foreach (var location in App.Locations)
            {
                var view = Instantiate(_locationViewPrefab, _parent);
                view.SetView(location);
            }
        }
    }
}