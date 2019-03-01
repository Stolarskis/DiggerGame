using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{

    public HealthBarScript HealthBar;
    private float currentHealth;
    private bool isEmergency;

    public HullObject[] hull;
    public int selectedHull = 0;

    public delegate void OutOfHealth();
    public static event OutOfHealth NoHealth;

    void Awake()
    {
        HealthBar.setupHealthBar();
        isEmergency = false;
        currentHealth = hull[selectedHull].maxHealth;
    }

    public void changeHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth <= 0)
        {
            NoHealth();
            currentHealth = 0;
        }
        float size = currentHealth / hull[selectedHull].maxHealth;
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

    public void replenishHealth()
    {
        currentHealth = hull[selectedHull].maxHealth;
        HealthBar.disableEmergency();
        HealthBar.setSize(1);
    }

    public float getMissingHealth()
    {
        return  hull[selectedHull].maxHealth - currentHealth;
    }

    public bool isFullHealth()
    {
        if (currentHealth == hull[selectedHull].maxHealth)
        {
            return true;
        }
        return false;
    }

}
 
