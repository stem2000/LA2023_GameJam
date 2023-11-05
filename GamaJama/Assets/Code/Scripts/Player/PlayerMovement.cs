using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // [Layers]
    // This number is from layers system and changes only of current Enemy layer is removed or reassigned
    private static int _enemyLayer = 7;

    // [Camera]
    private Camera _playerCamera;

    // [Movement]
    // Stores current move coroutine
    private Coroutine _moveCoroutine;
    private bool _isMoving = false;
    // Move destination parametres
    private Vector3 _movementDestination;
    private int _movementTargetLayer;
    private GameObject _movementTargetObject;
    // Movement balance variables
    [SerializeField]
    private float _playerSpeed = 10.0f;
    [SerializeField]
    private float _playerMoveFault = 0.1f;
    [SerializeField]
    private float _rotationSmoothTime = 100.01f;
    private float _currentVelocity;

    // [Events]
    public event EventHandler<EnemyPointParams> OnPlayerMoved;
    public event EventHandler<EnemyPointParams> OnPlayerFinishedMoving;

    public class EnemyPointParams : EventArgs
    {
        public Vector3 enemyPointCoorditates;
        public bool isLastPoint;
    }

    // The Awake function is called when the script instance is loaded.
    void Awake()
    {
        _playerCamera = Camera.main;
    }

    // Handles Move
    public void Move(InputAction.CallbackContext context)
    {
        GameObject movementDestination = HandleRaycastMovementDestination();
        
        //Assign ray hit point as a destination and replace running coroutine with new
        if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
        _moveCoroutine = StartCoroutine(PlayerMoveTowards(movementDestination, false));
    }

    // Handles InputSystem MoveToEnemyPoint
    public void MoveOnEnemyPoint(InputAction.CallbackContext context)
    {
        if(!_isMoving)
            HandleMove(false);
    }
    // Handles InputSystem Move - Right click
    public void MoveOnClosingPoint(InputAction.CallbackContext context)
    {
        if (!_isMoving)
            HandleMove(true);
    }

    private void HandleMove(bool IsLastPoint)
    {
        GameObject movementDestination = HandleRaycastMovementDestination();

        if (_movementTargetLayer == _enemyLayer)
        {
            //Assign ray hit point as a destination and replace running coroutine with new

            if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(PlayerMoveTowards(movementDestination, IsLastPoint));
            
            OnPlayerMoved?.Invoke(this, new EnemyPointParams 
            { enemyPointCoorditates = movementDestination.transform.position, isLastPoint = IsLastPoint });
        }
    }
    // Handles casting ray from camera to track mouse and stores collider layerId
    // Returns point clicked on screen
    private GameObject HandleRaycastMovementDestination()
    {
        Ray ray = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit)) 
        {
            _movementTargetLayer = raycastHit.collider.gameObject.layer;
            _movementTargetObject = raycastHit.collider.gameObject;
            _movementDestination = raycastHit.point;
        }
        return raycastHit.collider.gameObject;
    }

    // Handling rotation of player object in direction of movement
    private void ApplyRotation(Vector3 destination)
    {
        //if (destination.sqrMagnitude == 0) return;
        var targetAngle = Mathf.Atan2(destination.x, destination.z) * Mathf.Rad2Deg;
        var angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, _rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    // Applies player y(ground) offset
    private float ApplyMoveOffsets(Vector3 target)
    {
        return transform.position.y - target.y;
    }

    // Handles gradually moving target to the set destination
    private IEnumerator PlayerMoveTowards(GameObject objectDestination, bool IsLastPoint)
    {
        Vector3 movementDestination = objectDestination.transform.position;

        _isMoving = true;

        // runs each frame - tied to Time
        while (Vector3.Distance(transform.position, movementDestination) > _playerMoveFault)
        {
            movementDestination = objectDestination.transform.position;
            movementDestination.y += ApplyMoveOffsets(movementDestination);
            Vector3 direction = movementDestination - transform.position;
            ApplyRotation(direction.normalized);
            Vector3 destination = Vector3.MoveTowards(transform.position, movementDestination, _playerSpeed *Time.deltaTime);
            transform.position = destination;
            yield return null;
        }
        OnPlayerFinishedMoving?.Invoke(this, new EnemyPointParams
        { enemyPointCoorditates = movementDestination, isLastPoint = IsLastPoint });
        _isMoving = false;
    }

    // Spawns sphere in editor for debug purposes
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_movementDestination, 1);
    }

    private void OnTriggerEnter(Collider collider)
    {
       // create collision coords event
    }
}
