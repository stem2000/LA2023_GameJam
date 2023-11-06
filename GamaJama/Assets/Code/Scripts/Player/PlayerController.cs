using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public event Action<bool> OnPointReached;
    private PlayerMovement _playerMovement;
    private PlayerHealth _playerHealth;
    private GameObject _clickedObject;
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
        if(!_playerMovement.IsMoving) 
            _clickedObject = GetClickedObject();
        if (_clickedObject != null && _clickedObject.GetComponent<Enemy>())
        {
            if (!_playerMovement.IsMoving)
            {
                _playerMovement.Move(_clickedObject.transform);
                _pointStatus = pointStatus;
            }
        }
    }

    private GameObject GetClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

        if (hit2D.collider != null)
            return hit2D.collider.gameObject;
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerDamager = collision.gameObject.GetComponent<IPlayerDamager>();

        if (collision.gameObject == _clickedObject)
            OnPointReached?.Invoke(_pointStatus);

        if (playerDamager != null)
            _playerHealth.HealthDamage(playerDamager.GetDamage());
    }

}
