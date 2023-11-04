using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
        DrawStaticSegments();
        if(!_isClosedUp)
            DrawDynamicSegment();
    }

    private void DrawStaticSegments()
    {
        _lineRenderer.positionCount = _staticPoints.Count;
        _lineRenderer.SetPositions(_staticPoints.ToArray());
    }

    private void DrawDynamicSegment()
    {
        _lineRenderer.positionCount += 1;
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
        _lineRenderer.loop = true;
        _isClosedUp = true;

        FindCollectablesInside();
    }

    private void FindCollectablesInside()
    {
        //var collectables = FindObjectsOfType<ICollectable>();
        
        //foreach(var collectable in collectables)
        //{
        //    if(IsPointInside(collectable.GetPosition()))
        //        collectable.Collect();
        //}
    }

    public bool IsPointInside(Vector3 point)
    {
        bool parity = false;

        for(int i = 0, j = _staticPoints.Count - 1; i < _staticPoints.Count; j = i++)
        {
            if( 
                    ((_staticPoints[i].z <= point.z) && (point.z < _staticPoints[j].z) 
                    ||
                    (_staticPoints[j].z <= point.z) &&  (point.z < _staticPoints[i].z))
                &&
                    ((_staticPoints[j].z - _staticPoints[i].z != 0)
                    &&
                    (point.x > ((_staticPoints[j].x - _staticPoints[i].x) 
                        * 
                        (point.z - _staticPoints[i].z) / (_staticPoints[j].z - _staticPoints[i].z) + _staticPoints[i].x)))
            )
                parity = !parity;
        }

        return parity;
    }

}
