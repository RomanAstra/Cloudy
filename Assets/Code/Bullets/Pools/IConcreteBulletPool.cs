using UnityEngine;

namespace Cloudy
{
    public interface IConcreteBulletPool
    {
        void Spawn(Vector3 position, Quaternion rotation, Vector3 direction);
    }
}