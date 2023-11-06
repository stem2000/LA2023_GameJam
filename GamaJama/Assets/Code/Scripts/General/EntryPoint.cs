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
        SubscribeComponentsToEvents();
    }

    private void SubscribeComponentsToEvents()
    {
        SubscribeComponentsToGameManagerEvents();
        SubscribeComponentsToEnemyControllerEvents();
        SubscribeComponentsToPlayerEvents();
        SubscribeComponentsToUIEvents();
    }

    private void SubscribeComponentsToPlayerEvents()
    {
        _playerHealth.onPlayerDeath += _gameManagerScript.DefeatPlayer;
        _playerHealth.onPlayerHealthChanged += _gameManagerScript.PlayerHealthUI;
    }

    private void SubscribeComponentsToGameManagerEvents()
    {
        _gameManagerScript.GetTimerMaxValue += _playerHealth.HealthInitialGet;
    }

    private void SubscribeComponentsToEnemyControllerEvents()
    {
        _enemyController.OnAllUFOSDefeated += _gameManagerScript.WinPlayer;
    }

    private void SubscribeComponentsToUIEvents()
    {
        //_buttonsPause.pauseEvent += _gameManagerScript.PauseMenuView;
    }

}
