using UnityEngine;

namespace Cloudy
{
    public sealed class MovingCloudHierarchy : CloudHierarchy
    {
        public Transform BodyTransform;
        public Vector3[] TargetPoints;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            
            if(TargetPoints.Length < 2)
                return;
            
            for (var i = 0; i < TargetPoints.Length - 1; i++)
            {
                var pos = TargetPoints[i];
                var secondPos = TargetPoints[i + 1];
                Gizmos.DrawLine(pos, secondPos);
            }
        }
    }
}