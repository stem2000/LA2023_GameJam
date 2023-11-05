using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinaryEnemy : Enemy
{
    private EnemyMovement _movement;

    private void Start()
    {
        _movement = GetComponent<EnemyMovement>();    
    }

    protected override void SelfDestroy()
    {
        _movement.CanMove = false;
        Destroy(gameObject);
    }
}
