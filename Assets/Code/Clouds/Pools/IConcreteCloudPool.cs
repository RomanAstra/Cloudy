using UnityEngine;

namespace Cloudy.Pools
{
    public interface IConcreteCloudPool
    {
        void Spawn(Vector3 position);
    }
}