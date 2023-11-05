using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PolygonController _polygonController;
    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _playerController.Initialize();
        _polygonController.Initialize(_playerController.transform.position);

        SubscribeComponentsToPlayerEvents();
    }

    private void SubscribeComponentsToPlayerEvents()
    {
        _playerController.OnPointReached += _polygonController.AddPoint;
    }

}
