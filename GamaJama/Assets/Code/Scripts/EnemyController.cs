using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private List<Enemy> _ShipsList;

    public event EventHandler<Vector3> OnShipDropLoot;
    public event EventHandler OnAllUFOSDefeated;

    void Start()
    {
        foreach (var enemy in _ShipsList)
        {
            enemy.OnPlayerColision += ShipDied;
        }
    }
    private void ShipDied(object sender, OnPlColliArgs e)
    {
        OnShipDropLoot?.Invoke(this, e.ufoPosition);

        Enemy enemy = (Enemy)sender;
        _ShipsList.Remove(enemy);

        if (_ShipsList.Count == 0)
        {
            Debug.Log("All enemies are defeated!");
            OnAllUFOSDefeated?.Invoke(this, EventArgs.Empty);
        }
    }
    
    //to do:
    //red green light - need ship position and player hp
}

//this script gets the event that player collided with the ufo,
//and sends an event for player to take damage, and ufs's
//position for loot script to generate parts in its vicinity.
//then it deletes the ufo from the list and ends the game if
//there are no more ufos
