using Cloudy.Pools;

namespace Code
{
    public class AllEnemiesDamageSystem
    {
        private readonly CloudPoolFactory _cloudPoolFactory;
        private readonly int _damage;
        
        public AllEnemiesDamageSystem(CloudPoolFactory cloudPoolFactory, int damage)
        {
            _cloudPoolFactory = cloudPoolFactory;
            _damage = damage;
        }

        public void AllEnemiesDamage()
        {
            foreach (var pool in _cloudPoolFactory.Pools)
            {
                foreach (var cloud in pool.Clouds)
                {
                    cloud.DealDamage(_damage);
                }
            }
        }
    }
}