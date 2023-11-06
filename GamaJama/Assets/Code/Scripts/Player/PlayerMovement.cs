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

    public void Move(Transform target)
    {
        StartCoroutine(MoveToTarget(target));
    }

    private IEnumerator MoveToTarget(Transform target)
    {
        Vector3 movementDestination = target.position;

        _isMoving = true;

        while (target != null && Vector3.Distance(transform.position, movementDestination) > _playerMoveFault)
        {
            movementDestination = target.position;        
            ApplyRotation((movementDestination - transform.position).normalized);
            transform.position = Vector3.MoveTowards(transform.position, movementDestination, _playerSpeed * Time.deltaTime);
            yield return null;
        }

        _isMoving = false;
    }

    private void ApplyRotation(Vector3 destination)
    {
        var targetAngle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
        var angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, _rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }


}
