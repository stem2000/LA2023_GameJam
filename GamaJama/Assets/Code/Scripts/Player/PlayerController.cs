using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerHealth _playerHealth;
    private GameObject _clickedObject;
    private Vector3 _clickedPoint;
    private bool _pointStatus;

    public void Initialize()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void HandleLeftClick(InputAction.CallbackContext context)
    {
        HandleClick(false);
    }

    public void HandleRightClick(InputAction.CallbackContext context)
    {
        HandleClick(true);
    }

    private void HandleClick(bool pointStatus)
    {
        _clickedPoint = GetClickedPoint();
        if (!_playerMovement.IsMoving&& _clickedPoint!=null)
            _playerMovement.Move(_clickedPoint, pointStatus);  
    }

    private GameObject GetClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

        if (hit2D.collider != null)
            return hit2D.collider.gameObject;
        return null;
    }

    private Vector3 GetClickedPoint()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        return mouseWorldPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerDamager = collision.gameObject.GetComponent<IPlayerDamager>();

        if(playerDamager != null)
            _playerHealth.HealthDamage(playerDamager.GetDamage());
    }

}
