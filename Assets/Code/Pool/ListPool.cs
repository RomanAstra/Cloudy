using System.Collections.Generic;

namespace Utils
{
    public static class ListPool<T>
    {
        private static readonly ObjectPool<List<T>> _pool = new ObjectPool<List<T>>(null, Clear);

        private static void Clear(List<T> list)
        {
            list.Clear();
        }

        public static List<T> Get()
        {
            return _pool.Get();
        }
        
        public static void Get(out List<T> result)
        {
            result = _pool.Get();
        }

        public static void Release(List<T> toRelease)
        {
            _pool.Release(toRelease);
        }
    }
}
