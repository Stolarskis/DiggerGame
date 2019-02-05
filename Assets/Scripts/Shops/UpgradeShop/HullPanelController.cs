using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HullPanelController : MonoBehaviour
{
    
    public Text HullNameText;
    public Text HullCostText;
    public Text MaxHullText;

    public PlayerInventory playerInventory;
    public PlayerHealth playerHealth;
    public int selectedHull;

    public void DisplayAluminum()
    {
        selectedHull = 0;
        displayHull();
    }
    public void DisplaySteelHull()
    {
        selectedHull = 1;
        displayHull();
    }

    public void DisplayTitaniumHull()
    {
        selectedHull = 2;
        displayHull();
    }

    public void DisplayTungstenHull()
    {
        selectedHull= 3;
        displayHull();
    }

    public void DisplayNaniteHardenedHull()
    {
        selectedHull = 4;
        displayHull();
    }

    public void displayHull()
    {
        HullNameText.text = playerHealth.hull[selectedHull].name;
        HullCostText.text = "$" + playerHealth.hull[selectedHull].cost.ToString();
        MaxHullText.text = playerHealth.hull[selectedHull].maxHealth.ToString() + " Health";

    }

    public void BuyHull()
    {
        int cost = playerHealth.hull[selectedHull].cost;
        if(selectedHull == playerHealth.selectedHull)
        {
            Debug.Log("Already own Hull");
        }
        else if(playerInventory.getMoney()  <= cost)
        {
            Debug.Log("You Don't have enough Money");
        }
        else
        {
            playerInventory.addToMuny(-cost);
            playerHealth.selectedHull = selectedHull;
            playerHealth.replenishHealth();
        }

    }

}