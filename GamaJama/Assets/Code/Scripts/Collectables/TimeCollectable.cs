using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimeCollectable : MonoBehaviour, ICollectable
{
    private bool _state = true;
    private int _value = 5;
    public event EventHandler<int> OnBoosterTaken;

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public void Collected()
    {
        _state = false;
        OnBoosterTaken?.Invoke(this, _value);

        // дать пользователю хп
        Destroy(gameObject);
        // удалить обьект проиграть анимацию
    }
}
