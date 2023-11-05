using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Enemy : MonoBehaviour, IPlayerDamager
{
    [SerializeField] protected int _damage;
    protected bool _isDamageDone = false;

    public event Action<Enemy, Vector3> OnPlayerCollided;

    void Update()
    {
        if (_isDamageDone)
            Destroy(gameObject);
    }

    public int GetDamage()
    {
        OnPlayerCollided?.Invoke(this, transform.position);
        _isDamageDone = true;
        return _damage;
    }

    protected abstract void SelfDestroy();

}
