using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerFuel : MonoBehaviour
{
    public FuelBarScript FuelBar;
    public PlayerInventory playerInventory;
    public FuelStationController fuelStation; 
    public float currentFuel;
    private float emergencyThreshold = 0.3f;
    private bool isEmergency;

    public FuelTankObject[] fuelTanks;
    public int selectedFuelTank = 0;

    //Events
    public delegate void OutOfFuel();
    public static event OutOfFuel NoFuel;

    void Awake()
    {
        //FuelBar.SetupFuelBar();
        isEmergency = false;
        currentFuel = fuelTanks[selectedFuelTank].maxFuel; 
        StartCoroutine("useFuel");
        refillFuel();
    }

    IEnumerator useFuel()
    {
        while (true)
        {
            if (currentFuel <= 0)
            {
                StopCoroutine("useFuel");
                NoFuel();
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
            currentFuel = fuelTanks[selectedFuelTank].maxFuel;
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
        float size = currentFuel / fuelTanks[selectedFuelTank].maxFuel ;
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

    public float getMissingFuel()
    {
        return fuelTanks[selectedFuelTank].maxFuel - currentFuel;
    }
    
}
