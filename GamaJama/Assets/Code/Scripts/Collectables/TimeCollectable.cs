using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimeCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] private int _restoredTime = 5;
    public event Action<int> OnCollectableTaken;
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Collect()
    {
        OnCollectableTaken?.Invoke(_restoredTime);
        Destroy(gameObject);
    }
}