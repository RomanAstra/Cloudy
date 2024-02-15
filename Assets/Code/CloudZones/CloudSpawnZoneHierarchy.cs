using UnityEngine;

namespace Cloudy
{
    public sealed class CloudSpawnZoneHierarchy : MonoBehaviour
    {
        public string CloudName;
        public Vector2 SizeZone;
        public float SpawnDelay = 5f;
        public float StartSpawnDelay;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(SizeZone.x, SizeZone.y, 0.1f));
        }
    }
}