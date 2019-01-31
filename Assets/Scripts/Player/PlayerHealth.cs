using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public GameControllerScript GameController;
    public HealthBarScript HealthBar;
    public float maxHealth;
    private float currentHealth;
    private bool isEmergency;

    void Awake()
    {
        HealthBar.setupHealthBar();
        isEmergency = false;
        setMaxHealth(100);
    }

    public void changeHealth(float amount)
    {
        Debug.Log(amount);
        currentHealth += amount;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            GameController.enableGameOver();
            return;
        }
        float size = currentHealth / maxHealth;
        HealthBar.setSize(size);

        if (!isEmergency && size <= 0.2f)
        {
            HealthBar.enableEmergency();
        }
        if (isEmergency && size > 0.2f)
        {
            HealthBar.disableEmergency();
        }
    }

    public void setMaxHealth(float max)
    {
        maxHealth = max;
        currentHealth = maxHealth;
    }

    public void replenishHealth()
    {
        currentHealth = maxHealth;
        HealthBar.disableEmergency();
        HealthBar.setSize(1);
    }

    public float getMissingHealth()
    {
        return maxHealth - currentHealth;
    }

    public bool isFullHealth()
    {
        if (currentHealth == maxHealth)
        {
            return true;
        }
        return false;
    }

}
 
