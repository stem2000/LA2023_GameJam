using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PolygonController _polygonController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerHealth _playerHealth;
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
        var collectables = FindObjectsOfType<TimeCollectable>();

        foreach (var collectable in collectables)
        {
            collectable.OnCollectableTaken += _playerHealth.HealthRestore;
        }
    }

}
