using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CloudZoneDetector : MonoBehaviour
{
    private int _detectionCount;

    public bool HasDetection => _detectionCount > 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _detectionCount++;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _detectionCount--;
    }
}
