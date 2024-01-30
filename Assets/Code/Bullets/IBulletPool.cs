using UnityEngine;

namespace Cloudy
{
    public interface IBulletPool<T>
    {
        public T Get(Vector3 position, Quaternion rotation, Transform parent = null);
        public void Release(T poolObject);
    }
}