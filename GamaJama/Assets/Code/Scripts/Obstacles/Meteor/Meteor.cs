using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Meteor : MonoBehaviour, IPlayerDamager
{
    public event Action OnPlayerColision;
    [SerializeField] private int _damage = 20;

    public int GetDamage()
    {
        return _damage;
    }

}
