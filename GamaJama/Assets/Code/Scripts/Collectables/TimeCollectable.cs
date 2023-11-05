using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimeCollectable : MonoBehaviour, ICollectable
{
    private int _value = 5;
    public event Action<int> OnCollectableTaken;
    private PlayerHealth _playerHealth;

    public void Initialize()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }
    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public void Collect()
    {
        OnCollectableTaken?.Invoke( _value);
        Destroy(gameObject);
        // удалить обьект проиграть анимацию
    }
}