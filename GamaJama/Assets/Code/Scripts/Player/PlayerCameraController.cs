using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    // [Camera]
    private Camera _playerCamera;
    // [Player]
    [SerializeField] private GameObject target;
    [SerializeField] private float smoothTime = 0.25f;

    private Vector3 _offset;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
        _playerCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(0, 20, target.transform.position.z);
        _playerCamera.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
}
