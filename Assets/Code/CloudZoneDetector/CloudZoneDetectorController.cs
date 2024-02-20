using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class CloudZoneDetectorController : MonoBehaviour
{
    [SerializeField] private Vector2 _zone = new (8.5f, 4.5f);
    [SerializeField] private float _step = 0.5f;
    [SerializeField] private CloudZoneDetector _CloudZoneDetectorPrefab;

    private CloudZoneDetector[] _detectors;
    private int _detectionCount;

    public event Action AllZonesСaptured;
    
    private void Awake()
    {
        _detectors = GetComponentsInChildren<CloudZoneDetector>();
        foreach (var detector in _detectors)
        {
            detector.OnTriggerEnter += OnDetectorTriggerEnter;
            detector.OnTriggerExit += OnDetectorTriggerExit;
        }
    }
    private void OnDestroy()
    {
        foreach (var detector in _detectors)
        {
            detector.OnTriggerEnter -= OnDetectorTriggerEnter;
            detector.OnTriggerExit -= OnDetectorTriggerExit;
        }
    }
    private void OnDetectorTriggerEnter()
    {
        _detectionCount++;
        
        if(GetZoneProtectionProgress() == 0)
            AllZonesСaptured?.Invoke();
    }
    private void OnDetectorTriggerExit()
    {
        _detectionCount--;
    }

    public int GetZoneProtectionProgress()
    {
        return 100 - (int)(_detectionCount / (_detectors.Length / 100f));
    }
    
    [Button]
    private void CreateDetectors()
    {
        var width = _zone.x * 2;
        var height = _zone.y * 2;
        var cols = Mathf.RoundToInt(width / _step);
        var rows = Mathf.RoundToInt(height / _step);
        var count = cols * rows;
        
        var points = GetComponentsInChildren<CloudZoneDetector>().ToList();
        foreach (var meshPoint in points)
        {
            DestroyImmediate(meshPoint.gameObject);
        }
        
        for (var i = 0; i < count; i++)
        {
            var point = (CloudZoneDetector)UnityEditor.PrefabUtility.InstantiatePrefab(_CloudZoneDetectorPrefab, transform);
            
            var posX = i % cols * _step;
            var posY = i / cols * _step;
        
            var pos = new Vector3(posX + (_step - width) * 0.5f, posY + (_step - height) * 0.5f);
            point.transform.localPosition = pos;
            point.name = "Detector " + (i + 1);
        }
    }
}
