using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CloudZoneDetector : MonoBehaviour
{
    public Action OnTriggerEnter;
    public Action OnTriggerExit;
    
    private int _detectionCount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_detectionCount == 0)
            OnTriggerEnter?.Invoke();
        
        _detectionCount++;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _detectionCount--;
        
        if(_detectionCount == 0)
            OnTriggerExit?.Invoke();
    }
}
