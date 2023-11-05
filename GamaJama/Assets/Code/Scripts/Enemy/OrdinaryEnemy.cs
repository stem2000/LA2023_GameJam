using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinaryEnemy : Enemy
{
    private EnemyMovement _movement;
    protected override void SelfDestroy()
    {
        _movement.CanMove = false;
        Destroy(this.gameObject);
    }
}
