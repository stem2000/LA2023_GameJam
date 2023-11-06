using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 10.0f;
    [SerializeField] private float _playerMoveFault = 0.1f;
    [SerializeField] private float _rotationSmoothTime = 100.01f;

    public bool IsMoving { get { return _isMoving; } }

    private bool _isMoving = false;
    public event Action<Vector3, bool> OnPointReached;

    public void Move(Vector3 movementDestination, bool _pointStatus)
    {
        StartCoroutine(PlayerMoveTowards(movementDestination, _pointStatus));
    }

    private IEnumerator PlayerMoveTowards(Vector3 target, bool _pointStatus)
    {
        Vector3 movementDestination = new Vector3(target.x, target.y, 0);
        _isMoving = true;
        while (Vector3.Distance(transform.position, movementDestination) >  _playerMoveFault)
        {
            Vector3 direction = movementDestination - transform.position;
            ApplyRotation(direction.normalized);
            Vector3 destination = Vector3.MoveTowards(transform.position, movementDestination, _playerSpeed * Time.deltaTime);
            transform.position = destination;
            yield return null;
        }
        OnPointReached?.Invoke(movementDestination, _pointStatus);
        _isMoving = false;
    }

    private void ApplyRotation(Vector3 destination)
    {
        var targetAngle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
        var angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, _rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }


}
