using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Enemy : MonoBehaviour, IPlayerDamager
{
    private Vector3 _Myposition;
    private float _damage;

    public event EventHandler<OnPlColliArgs> OnPlayerColision;
    public class OnPlColliArgs : EventArgs
    {
        public Vector3 ufoPosition;
    }

    protected float _Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    void Start()
    {
        _Myposition = transform.position;
    }

    public float GetDamage()
    {
        OnPlayerColision?.Invoke(this, new OnPlColliArgs { ufoPosition = _Myposition });
        Debug.Log(_damage);
        //some animation before it's destroted?
        Destroy(gameObject);
        return _damage;
    }

    //private void OnTriggerEnter(Collider other) //detect collision with player by cheaking tag, then act accordingly to the ufo type
    //{
    //    if (other.CompareTag("Player")){
           
    //    }
    //}

   
}
