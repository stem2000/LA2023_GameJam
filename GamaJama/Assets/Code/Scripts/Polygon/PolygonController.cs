using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class PolygonController : MonoBehaviour
{
    [SerializeField] private float _timeBeforeDestroy = 1f;

    private List<Vector3> _polyPoints;
    private LineRenderer _lineRenderer;
    
    public void Initialize(Vector3 startPoint)
    {
        _polyPoints = new List<Vector3>(){ startPoint };
        _lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    void Update()
    {
        DrawPolygon();
    }

    private void DrawPolygon()
    {
        UpdateSegments(_lineRenderer);
    }

    private void UpdateSegments(LineRenderer lineRenderer)
    {
        lineRenderer.positionCount = _polyPoints.Count;
        lineRenderer.SetPositions(_polyPoints.ToArray());
    }

    public void AddPoint(Vector3 point, bool isEndPoint)
    {
        _polyPoints.Add(point);

        if(isEndPoint)
            CloseUpPolygon();
    }

    private void CloseUpPolygon()
    {
        StartCoroutine(DisplayPolyCopy(Instantiate(_lineRenderer, transform)));
        FindCollectablesInside();
        ResetPoints();
    }

    private void ResetPoints()
    {
        var lastPoint = _polyPoints.Last();

        _polyPoints.Clear();
        _polyPoints.Add(lastPoint);
    }

    private IEnumerator DisplayPolyCopy(LineRenderer polyCopy)
    {
        UpdateSegments(polyCopy);
        polyCopy.loop = true;

        yield return new WaitForSeconds(_timeBeforeDestroy);
        Destroy(polyCopy);
    }

    private void FindCollectablesInside()
    {
        var collectables = FindObjectsOfType<MonoBehaviour>().OfType<ICollectable>();

        foreach (var collectable in collectables)
        {
            if (IsPointInside(collectable.GetPosition()))
                collectable.Collect();
        }
    }

    public bool IsPointInside(Vector3 point)
    {
        bool parity = false;

        for(int i = 0, j = _polyPoints.Count - 1; i < _polyPoints.Count; j = i++)
        {
            if( 
                (
                    ((_polyPoints[i].y <= point.y) && (point.y < _polyPoints[j].y))
                        ||
                    ((_polyPoints[j].y <= point.y) && (point.y < _polyPoints[i].y))
                )
                &&
                (
                    point.x > (_polyPoints[j].x - _polyPoints[i].x) *
                        (point.y - _polyPoints[i].y) / (_polyPoints[j].y - _polyPoints[i].y) + _polyPoints[i].x
                )
            )
                parity = !parity;
        }

        return parity;
    }

}
