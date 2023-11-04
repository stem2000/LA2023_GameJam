using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    // Move destination parametres
    private Vector3 _movementDestination;
    private int _movementTargetLayer;
    // Movement balance variables
    [SerializeField]
    private float _playerSpeed = 10.0f;
    [SerializeField]
    private float _playerMovementFault = 0.1f;

    // The Awake function is called when the script instance is loaded.
    void Awake()
    {
        _playerCamera = Camera.main;
    }

    // Handles InputSystem Move
    public void Move(InputAction.CallbackContext context)
    {
        Vector3 movementDestination = HandleRaycastMovementDestination();
        //Assign ray hit point as a destination and replace running coroutine with new
        if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
        _moveCoroutine = StartCoroutine(PlayerMoveTowards(movementDestination));
    }

    // Handles InputSystem MoveToObject
    public void MoveToObject(InputAction.CallbackContext context)
    {
        Vector3 movementDestination= HandleRaycastMovementDestination();

        if (_movementTargetLayer == _enemyLayer)
        {
            //Assign ray hit point as a destination and replace running coroutine with new
            if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(PlayerMoveTowards(movementDestination));
        }
    }

    // Handles casting ray from camera to track mouse and stores collider layerId
    // Returns point clicked on screen
    private Vector3 HandleRaycastMovementDestination()
    {
        Ray ray = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit)) 
        {
            _movementTargetLayer = raycastHit.collider.gameObject.layer;
            _movementDestination = raycastHit.point;
        }
        return raycastHit.point;
    }

    // Handles gradually moving target to the set destination
    private IEnumerator PlayerMoveTowards(Vector3 target)
    {
        // runs each frame - tied to Time
        while (Vector3.Distance(transform.position, target) > _playerMovementFault)
        {
            Vector3 destination = Vector3.MoveTowards(transform.position, target, _playerSpeed*Time.deltaTime);
            transform.position = destination;
            yield return null;
        }
    }

    // Spawns sphere in editor for debug purposes
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_movementDestination, 1);
    }

}
