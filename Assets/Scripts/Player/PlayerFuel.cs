using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerFuel : MonoBehaviour
{
    public FuelBarScript FuelBar;
    public GameControllerScript GameController;
    public PlayerInventory playerInventory;
    public FuelStationController fuelStation; 
    public float maxFuel;
    public float currentFuel;
    private float emergencyThreshold = 0.3f;
    private bool isEmergency;
    

    void Awake()
    {
        FuelBar.SetupFuelBar();
        isEmergency = false;
        setMaxFuel(1000);
        StartCoroutine("useFuel");
    }

    IEnumerator useFuel()
    {
        while (true)
        {
            if (currentFuel <= 0)
            {
                StopCoroutine("useFuel"); 
                GameController.enableGameOver();
            }
            else
            {
                decrementFuel();
            }

            yield return new WaitForSeconds(1);
        }

    }

    public float getFuelLevel()
    {
        return currentFuel;
    }

    public void refillFuel()
    {
            currentFuel = maxFuel;
            FuelBar.setSize(1);
            if (isEmergency)
            {
                FuelBar.disableEmergency();
                isEmergency = false;
            }
    }

    private void decrementFuel()
    {
        currentFuel--;
        float size = currentFuel / maxFuel;
        FuelBar.setSize(size);

        if(!isEmergency && size <= emergencyThreshold)
        {
            FuelBar.enableEmergency();
            isEmergency = true;
        }
        if (isEmergency && size > emergencyThreshold)
        {
            FuelBar.disableEmergency();
            isEmergency = false;
        }
    }

    public void setMaxFuel(float max)
    {
        maxFuel = max;
        currentFuel = maxFuel;
    }

    public float getMissingFuel()
    {
        return maxFuel - currentFuel;
    }
    
}
