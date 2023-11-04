using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PolygonTester : MonoBehaviour
{
    [SerializeField] private PolygonController _polyController;
    [SerializeField] private Transform _movingPoint;
   

    private void Start()
    {
        _polyController.Initialize(_movingPoint.transform);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _polyController.AddPoint(_movingPoint.position, false);
        }
        if(Input.GetMouseButtonDown(1))
        {
            _polyController.AddPoint(_movingPoint.position, true);
        }
    }
}
