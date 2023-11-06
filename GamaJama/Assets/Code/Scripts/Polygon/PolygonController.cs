using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class PolygonController : MonoBehaviour
{
    [SerializeField] private float _timeBeforeDestroy = 1f;
    [SerializeField] private Transform _polyTarget;

    private List<Vector3> _polyPoints;
    private LineRenderer _lineRenderer;
    private bool _isClosedUp = false;
    
    public void Initialize(Transform target)
    {
        _polyTarget = target;

        _polyPoints = new List<Vector3>(){ _polyTarget.position };
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

        if (!_isClosedUp)
        {
            lineRenderer.positionCount += 1;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, _polyTarget.position);
        }
    }

    public void AddPoint(bool isEndPoint)
    {
        _polyPoints.Add(_polyTarget.position);

        if (isEndPoint)
        {
            _isClosedUp = true;
            CloseUpPolygon();
        }
    }

    private void CloseUpPolygon()
    {
        StartCoroutine(DisplayPolyCopy(Instantiate(_lineRenderer, transform)));
        FindCollectablesInside();
        ResetPoints();
        _isClosedUp = false;
    }

    private void ResetPoints()
    {
        _polyPoints.Clear();
        _polyPoints.Add(_polyTarget.position);
    }

    private IEnumerator DisplayPolyCopy(LineRenderer polyCopy)
    {
        UpdateSegments(polyCopy);
        polyCopy.loop = true;

        yield return new WaitForSeconds(_timeBeforeDestroy);
        Destroy(polyCopy.gameObject);
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
