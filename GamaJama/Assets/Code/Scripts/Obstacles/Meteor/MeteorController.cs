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

    private void MeteorCollide(object sender, EventArgs e)
    {
        OnDestroyPath?.Invoke(this, EventArgs.Empty);
        Debug.Log("meteor");
    }
}

