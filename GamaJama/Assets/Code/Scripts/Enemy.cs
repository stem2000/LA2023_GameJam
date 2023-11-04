using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Enemy : MonoBehaviour, IPlayerDamager
{
    public enum ufoType //list of the variations of the ufo
    {
        Ordinary,
        Bomb
    }
    [SerializeField] ufoType _myType;
    //[SerializeField] float _OrdinaryDamage = 10f;
    //[SerializeField] float _BombDamage = 15f;
    private Vector3 _Myposition;
    private float _damage;

    public event EventHandler<OnPlColliArgs> OnPlayerColision;
    public class OnPlColliArgs : EventArgs
    {
        public Vector3 ufoPosition;
    }

    void Start()
    {
        _Myposition = transform.position;
    }

    public float GetDamage()
    {
        return _damage;
    }

    private void OnTriggerEnter(Collider other) //detect collision with player by cheaking tag, then act accordingly to the ufo type
    {
        if (other.CompareTag("Player")){
            OnPlayerColision?.Invoke(this, new OnPlColliArgs { ufoPosition = _Myposition });
            Debug.Log(_damage);
            //some animation before it's destroted?
            Destroy(gameObject);
        }
    }

   
}
