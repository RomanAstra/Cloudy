using Cloudy.Configs;

namespace Cloudy
{
    public sealed class FreezingBulletAdapter : BulletAdapter
    {
        private readonly float _freezingTime;
        
        public FreezingBulletAdapter(BulletHierarchy hierarchy, WeaponUpgradeSystem weaponUpgradeSystem, 
            FreezingBulletConfig config, string id) : base(hierarchy, weaponUpgradeSystem, config, id)
        {
            _hierarchy.onTriggerEnter += OnTriggerEnter;
            
            _freezingTime = config.FreezingTime + weaponUpgradeSystem.GetValue(_id, StatType.FreezingTime);
        }
        
        protected override void Release()
        {
            base.Release();
            _hierarchy.onTriggerEnter -= OnTriggerEnter;
        }
        private void OnTriggerEnter(CloudHierarchy cloud)
        {
            cloud.SetFrozen(_freezingTime);
        }
    }
}