using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPlayerDamager
{
    public enum ufoType //list of the variations of the ufo
    {
        Ordinary,
        Bomb
    }
    [SerializeField] ufoType _myType;
    [SerializeField] float _OrdinaryDamage = 10f;
    [SerializeField] float _BombDamage = 15f;
    private Vector3 _Myposition;

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
        if (_myType == ufoType.Ordinary)
        {
            return _OrdinaryDamage;
        }
        else if (_myType == ufoType.Bomb)
        {
            return _BombDamage;
        }
        else
        {
            return 0f;
        }
    }

    private void OnTriggerEnter(Collider other) //detect collision with player by cheaking tag, then act accordingly to the ufo type
    {
        if (other.CompareTag("Player")){
            OnPlayerColision?.Invoke(this, new OnPlColliArgs { ufoPosition = _Myposition });
            //some animation before it's destroted?
            Destroy(gameObject);
        }
    }

   
}
