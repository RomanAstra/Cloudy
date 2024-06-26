using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private bool _isActive;

    private LineRenderer _lineRendererComponent;
    private Transform _transform;

    private void Awake()
    {
        _lineRendererComponent = GetComponent<LineRenderer>();
        _lineRendererComponent.positionCount = 5;
        _transform = transform;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.R))
            _isActive = !_isActive;
#endif
        
        _lineRendererComponent.enabled = _isActive;
        if(!_isActive)
            return;
        
        var points = new Vector3[_lineRendererComponent.positionCount];

        var direction = transform.right;
        var tempPosition = _transform.position;;
        points[0] = tempPosition;
        
        for (var i = 1; i < points.Length; i++)
        {
            if (Physics.Raycast(tempPosition, direction, out var hit, 20, _layerMask))
            {
                direction = Vector3.Reflect(direction, hit.normal);
                tempPosition = hit.point;
            }

            points[i] = tempPosition;
        }
        
        _lineRendererComponent.SetPositions(points);
    }
}
