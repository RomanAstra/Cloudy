using Cloudy.Configs;

namespace Cloudy
{
    public sealed class StrengtheningBulletAdapter : BulletAdapter
    {
        private readonly WeaponUpgradeSystem _weaponUpgradeSystem;
        private int _strengthening;
        
        public StrengtheningBulletAdapter(BulletHierarchy hierarchy, WeaponUpgradeSystem weaponUpgradeSystem,
            StrengtheningBulletConfig config, string id) : base(hierarchy, weaponUpgradeSystem, config, id)
        {
            _hierarchy.onTriggerEnter += OnTriggerEnter;
            _weaponUpgradeSystem = weaponUpgradeSystem;
            _strengthening = config.Strengthening;
        }
        
        protected override void Release()
        {
            base.Release();
            _hierarchy.onTriggerEnter -= OnTriggerEnter;
            _strengthening = 0;
        }
        private void OnTriggerEnter(CloudHierarchy cloud)
        {
            cloud.DealDamage(_strengthening);

            _strengthening++;
            _strengthening += (int)_weaponUpgradeSystem.GetValue(_id, StatType.Strengthening);
        }
    }
}