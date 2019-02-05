using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FuelTankPanelController: MonoBehaviour
{
    
    public Text FuelTankNameText;
    public Text FuelTankCostText;
    public Text MaxFuelText;

    public PlayerInventory playerInventory;
    public PlayerFuel playerFuel;
    public int selectedFuelTank;

    public void DisplaySmallFuelTank()
    {
        selectedFuelTank = 0;
        displayFuelTank();
    }

    public void displaymedFueltank()
    {
        selectedFuelTank = 1;
        displayFuelTank();
    }
  
     public void DisplayLargeFuelTank()
    {
        selectedFuelTank = 2;
        displayFuelTank();
    }
    

    public void displayFuelTank()
    {
        FuelTankNameText.text =  playerFuel.fuelTanks[selectedFuelTank].name;
        FuelTankCostText.text = "$" + playerFuel.fuelTanks[selectedFuelTank].cost.ToString();
        MaxFuelText.text = "Fuel Capacity: " + playerFuel.fuelTanks[selectedFuelTank].maxFuel.ToString();
    }

    public void BuyFueltank()
    {
        int cost = playerFuel.fuelTanks[selectedFuelTank].cost;
        if(selectedFuelTank == playerFuel.selectedFuelTank)
        {
            Debug.Log("Already own Fuel Tank");
        }
        else if(playerInventory.getMoney()  <= cost)
        {
            Debug.Log("You Don't have enough Money");
        }
        else
        {
            playerInventory.addToMuny(-cost);
            playerFuel.selectedFuelTank = selectedFuelTank;
            playerFuel.refillFuel();
        }

    }

}