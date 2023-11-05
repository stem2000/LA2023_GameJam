using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PolygonController _polygonController;
    [SerializeField] private PlayerMovement _playerMovement;

    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _polygonController.Initialize(_playerMovement.transform);

        SubscribeComponentsToPlayerEvents();
    }

    private void SubscribeComponentsToPlayerEvents()
    {

    }

}
