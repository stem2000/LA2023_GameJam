using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // [Health]
    private int healthCurrent;
    [SerializeField]
    private int healthInitial = 60;
    // [Events]

    [Header("Events")]

    public event EventHandler<int> PlayerHealthChanged;

    // [Timer]
    private WaitForSeconds waitForSeconds;

    // Start is called before the first frame update
    private void Awake()
    {
        HealthReset();
        waitForSeconds = new WaitForSeconds(1.0f);
        StartCoroutine(HealthDrain(1));
        //InvokeRepeating("HealthDrain", 0, 1.0f);
    }
    // Sets player's current health back to its initial value
    public void HealthReset()
    {
        healthCurrent = healthInitial;
        onPlayerHealthChanged.Invoke();
    }

    // Reduces player's current health
    public void HealthDamage(int damageAmount)
    {
        healthCurrent -= damageAmount;
        onPlayerHealthChanged.Invoke();
        if (healthCurrent <= 0)
        { 
            // вызвать смерть
        }
    }

    //Increases player's current health
    public void HealthRestore(int healAmount)
    {
        healthCurrent += healAmount;
        onPlayerHealthChanged.Invoke();
    }

    IEnumerator HealthDrain(int amount)
    {
        HealthDamage(amount);
        Debug.Log("Player current hp: "+healthCurrent);
        //play animation
        yield return waitForSeconds;
        StartCoroutine(HealthDrain(1));
    }

    protected virtual void OnPlayerHealthChanged(EventArgs e)
    {
        PlayerHealthChanged?.Invoke(this, e);
    }
    protected virtual void OnPlayerDeath(EventArgs e)
    {
        Debug.Log("Dead " + healthCurrent);
        // Invoke interface and death animations things
    }
}
