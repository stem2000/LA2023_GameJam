using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonController : MonoBehaviour
{
    private List<Vector3> _staticPoints;
    private Transform _startingPoint;
    private Transform _movingPoint;
    private LineRenderer _lineRenderer;
    
    void Start()
    {
        _staticPoints = new List<Vector3>();
        _lineRenderer = GetComponent<LineRenderer>();        
    }

    void Update()
    {
        
    }

    public void AddPoint(Vector3 point, bool isEndPoint)
    {
        if (isEndPoint)
        {
            _staticPoints.Add(point);
        }
    }

    private bool CheckIfPolyClosedUp(Vector3 point)
    {
        if(point == _startingPoint.position)
            return true;
        else
            return false;
    }

    private void DrawPolygon()
    {
        for(int i = 0; i < _staticPoints.Count; i++)
            _lineRenderer.SetPosition(i, _staticPoints[i]);
    }
}
