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
    public event EventHandler<int> onPlayerHealthChanged;
    public event EventHandler onPlayerDeath;

    private void Start()
    {
        HealthReset();
    }
    // Sets player's current health back to its initial value
    public void HealthReset()
    {
        _healthCurrent = _healthInitial;
        onPlayerHealthChanged?.Invoke(this, _healthCurrent);
        // sets hp timer that lowers every second
        InvokeRepeating("HealthDrain", 0, 1.0f);
    }

    // Reduces player's current health
    public void HealthDamage(int damageAmount)
    {
        _healthCurrent -= damageAmount;
        onPlayerHealthChanged?.Invoke(this, _healthCurrent);
        if (_healthCurrent <= 0)
        {
            onPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    //Increases player's current health
    public void HealthRestore(int healAmount)
    {
        _healthCurrent += healAmount;
        onPlayerHealthChanged?.Invoke(this, _healthCurrent);
    }

    private void HealthDrain()
    {
        HealthDamage(_healthDrainPerSecond);
        Debug.Log("Player hp: " + _healthCurrent);
    }
}
