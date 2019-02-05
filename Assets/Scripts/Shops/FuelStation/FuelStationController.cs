using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FuelStationController : MonoBehaviour
{

    public Canvas fuelStationCanvas;
    public Text totalText;
    public PlayerFuel playerFuel;
    public PlayerInventory playerInventory;

    void OnTriggerEnter2D(Collider2D col)
    {
        setTotalText(playerFuel.getMissingFuel().ToString());
        if (col.gameObject.CompareTag("Player"))
        {
            openFuelGUI();
        }
    }

    private void openFuelGUI()
    {
        fuelStationCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void closeCanvas()
    {
        fuelStationCanvas.enabled = false;
        Time.timeScale = 1;
    }

    public void setTotalText(string text)
    {
        totalText.text = "Total: " + text; 
    }

    //Fuel cost is 1:1, ie, 1 missing fuel costs $1
    public void buyFuel(){
        long pMoney = playerInventory.getMoney();
        float missingFuel = playerFuel.fuelTanks[playerFuel.selectedFuelTank].maxFuel - playerFuel.currentFuel;
        if (pMoney < missingFuel)
        {
            setTotalText("Not Enough Money!");
        }
        else
        {
            playerInventory.addToMuny(-Convert.ToInt64(missingFuel));
            setTotalText("Thank You");
            playerFuel.refillFuel();
        }
     }
}
