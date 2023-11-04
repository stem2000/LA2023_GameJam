using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class MeteorController : MonoBehaviour
{
    [SerializeField] private List<Meteor> _ShipsList;

    public event EventHandler<float> OnPlaTakeDamageandStop;
    public event EventHandler OnDestroyPath;
    [SerializeField] float _MeteorDamage = 20f;
    void Start()
    {
        foreach (var enemy in _ShipsList)
        {
            enemy.OnPlayerColision += MeteorCollide;
        }
    }

    private void MeteorCollide(object sender, EventArgs e)
    {
        OnPlaTakeDamageandStop?.Invoke(this, _MeteorDamage);
        OnDestroyPath?.Invoke(this, EventArgs.Empty);
        Debug.Log(_MeteorDamage);
        Debug.Log("meteor");
    }
}

//this script gets the event that player colided with a meteor,
//and then sendt a message to plaer to take damadge and stop, and
//to the script that draws path to abort drawing
