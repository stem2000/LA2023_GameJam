using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PolygonController : MonoBehaviour
{
    private List<Vector3> _staticPoints;
    private Transform _dynamicPoint;
    private LineRenderer _lineRenderer;
    private bool _isClosedUp = false;
    
    public void Initialize(Transform movingPoint)
    {
        _dynamicPoint = movingPoint;
        _staticPoints = new List<Vector3>(){ _dynamicPoint.position };
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawPolygon();
    }

    private void DrawPolygon()
    {
        _lineRenderer.positionCount = _staticPoints.Count + 1;
        DrawStaticSegments();
        if(!_isClosedUp)
            DrawDynamicSegment();
    }

    private void DrawStaticSegments()
    {
        _lineRenderer.SetPositions(_staticPoints.ToArray());
    }

    private void DrawDynamicSegment()
    {
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _dynamicPoint.position);
    }

    public void AddPoint(Vector3 point, bool isEndPoint)
    {
        _staticPoints.Add(point);

        if(isEndPoint)
            CloseUpPolygon();
    }

    private void CloseUpPolygon()
    {
        _staticPoints.Add(_staticPoints[0]);
        _isClosedUp = true;
    }
}
