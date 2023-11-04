using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Meteor : MonoBehaviour
{
    public event EventHandler OnPlayerColision;
    private void OnTriggerEnter(Collider other) //detect collision with player by cheaking tag
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerColision?.Invoke(this, EventArgs.Empty);
        }
    }
}
