using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PolygonController _polygonController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerHealth _playerHealth;

    [SerializeField] private EnemyController _enemyController;

    [SerializeField] private GameManagerScript _gameManagerScript;
    [SerializeField] private UIButtons _buttonsPause;
    private void Awake()
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
        _playerHealth.onPlayerDeath += _gameManagerScript.DefeatPlayer;
        _gameManagerScript.GetTimerMaxValue += _playerHealth.HealthInitialGet;
        _playerHealth.onPlayerHealthChanged += _gameManagerScript.PlayerHealthUI;
        _enemyController.OnAllUFOSDefeated += _gameManagerScript.WinPlayer;
        _buttonsPause.pauseEvent += _gameManagerScript.PauseMenuView;

        _playerMovement.OnPointReached += _polygonController.AddPoint;
        var collectables = FindObjectsOfType<TimeCollectable>();

        foreach (var collectable in collectables)
        {
            collectable.OnCollectableTaken += _playerHealth.HealthRestore;
        }
    }

}
