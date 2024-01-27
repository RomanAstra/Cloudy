using UnityEngine;

namespace Utils
{
    public static class TransformExtensions
    {
        public static void AddScale(this Transform target, float value)
        {
            SetScale(target, target.localScale.x + value);
        }
        
        public static void SetScale(this Transform target, float value)
        {
            target.localScale = new Vector3(value, value, value);
        }

        public static string GetHierarchyPath(this Transform target)
        {
            if (target != target.root)
            {
                return target.parent.GetHierarchyPath() + "/" + target.name;
            }
            return target.name;
        }
    }
}
