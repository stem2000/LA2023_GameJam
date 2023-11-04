using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform[] _waypoints; 
    [SerializeField] float _speed = 2.0f; 

    private int _currentWaypointIndex = 0;
    private Transform _currentWaypoint;

    void Start()
    {
        if (_waypoints.Length > 0)
        {
            _currentWaypoint = _waypoints[_currentWaypointIndex];
        }
    }

    void Update()
    {
        if (_waypoints.Length == 0)
        {
            return; 
        }
        Vector3 direction = _currentWaypoint.position - transform.position;

        transform.Translate(direction.normalized * _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _currentWaypoint.position) < 0.1f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            _currentWaypoint = _waypoints[_currentWaypointIndex];
        }
    }
}
