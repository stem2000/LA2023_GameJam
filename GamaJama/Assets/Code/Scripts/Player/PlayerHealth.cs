using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static TimeCollectable;

public class PlayerHealth : MonoBehaviour
{
    // [Health]
    private int _healthCurrent;
    [SerializeField]
    private int _healthInitial = 60;
    [SerializeField]
    private int _healthDrainPerSecond = 1;

    // [Events]
    public event Action<int> onPlayerHealthChanged;
    public  event Action onPlayerDeath;

    private void Start()
    {
        HealthReset();
    }
    public int HealthInitialGet()
    {
        return _healthInitial;
    }

    // Sets player's current health back to its initial value
    public void HealthReset()
    {
        _healthCurrent = _healthInitial;
        onPlayerHealthChanged?.Invoke(_healthCurrent);
        // sets hp timer that lowers every second
        InvokeRepeating("HealthDrain", 0, 1.0f);
    }

    // Reduces player's current health
    public void HealthDamage(int damageAmount)
    {
        _healthCurrent -= damageAmount;
        onPlayerHealthChanged?.Invoke(_healthCurrent);
        if (_healthCurrent <= 0)
        {
            onPlayerDeath?.Invoke();
        }

        Debug.Log("Player hp: " + _healthCurrent);
    }

    //Increases player's current health
    public void HealthRestore(int healAmount)
    {
        _healthCurrent += healAmount;
        onPlayerHealthChanged?.Invoke(_healthCurrent);
        Debug.Log("Player healed: " + _healthCurrent);
    }

    private void HealthDrain()
    {
        HealthDamage(_healthDrainPerSecond);
    }
}
