using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinaryEnemy : Enemy
{
    [SerializeField] float damage = 10f;
    private void Start()
    {
        _Damage = damage;
    }

}
