using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemy
{
    [SerializeField] float damage = 15f;
    private void Start()
    {
        _Damage = damage;
    }
}

