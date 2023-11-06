using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private List<Enemy> _enemyList;

    public event EventHandler<Vector3> OnShipDropLoot;
    public event Action OnAllUFOSDefeated;

    void Start()
    {
        foreach (var enemy in _enemyList)
        {
            enemy.OnPlayerCollided += HandleEnemyDestroy;
        }
    }
    private void HandleEnemyDestroy(Enemy sender, Vector3 collisionPosition)
    {
        _enemyList.Remove(sender);

        if (_enemyList.Count == 0)
        {
            OnAllUFOSDefeated?.Invoke();
        }
    }

}

