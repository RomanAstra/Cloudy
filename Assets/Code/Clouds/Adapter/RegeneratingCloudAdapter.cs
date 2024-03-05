using Cloudy.Configs;
using UnityEngine;

namespace Cloudy.Adapter
{
    public sealed class RegeneratingCloudAdapter : CloudAdapter
    {
        private readonly RegeneratingCloudConfig _config;
        private readonly Countdown _regeneratingDelay = new ();
        
        public RegeneratingCloudAdapter(CloudHierarchy hierarchy, RegeneratingCloudConfig config) : base(hierarchy, config)
        {
            _config = config;

            _regeneratingDelay.Duration = _config.RegeneratingDelay;
        }
        
        private void AddHitPoints()
        {
            SetHitPoints(Mathf.Min(_currentHitPoints + _config.HitPointsRegeneratingCount, 
                _config.HitPoints));
        }
        
        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            
            if(_currentHitPoints == _config.HitPoints)
                return;
            
            _regeneratingDelay.Update();
            
            if(!_regeneratingDelay.IsEnded)
                return;
            
            AddHitPoints();
            _regeneratingDelay.Reset();
        }
    }
}