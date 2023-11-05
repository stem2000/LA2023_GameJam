using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Meteor : MonoBehaviour, IPlayerDamager
{
    public event EventHandler OnPlayerColision;
    [SerializeField] float _MeteorDamage = 20f;
    //private void OnTriggerEnter(Collider other) //detect collision with player by cheaking tag
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        OnPlayerColision?.Invoke(this, EventArgs.Empty);
    //    }
    //}
    public float GetDamage()
    {
        OnPlayerColision?.Invoke(this, EventArgs.Empty);
        return _MeteorDamage;
    }
}
