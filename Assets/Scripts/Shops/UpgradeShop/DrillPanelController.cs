using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DrillPanelController : MonoBehaviour
{
    
    public Text DrillNameText;
    public Text DrillInfoText;
    public Text DrillCostText;
    public Text DrillDigRateText;

    public PlayerInventory playerInventory;
    public playerMovement playerMovement;
    public int selectedDrill;

    public Hashtable drillsNames = new Hashtable()
    {
        {"CopperDrill",0 },
        {"MegaUltraDrill",1 },
    };

    public void DisplayCopperDrill()
    {

        displayDrillInfo("CopperDrill");
        selectedDrill = 0;
    }
    
    public void DisplayMegaUltraDrill()
    {
        displayDrillInfo("MegaUltraDrill");
        selectedDrill = 1;
    }

    public void displayDrillInfo(string drillName)
    {
        DrillNameText.text = "Type: " + playerMovement.drills[Convert.ToUInt16(drillsNames[drillName])].name;
        DrillInfoText.text = playerMovement.drills[Convert.ToUInt16(drillsNames[drillName])].description;
        DrillCostText.text = "Cost: $" + playerMovement.drills[Convert.ToUInt16(drillsNames[drillName])].cost.ToString();
        DrillDigRateText.text = "DigRate: " + playerMovement.drills[Convert.ToUInt16(drillsNames[drillName])].digRate.ToString();
    }

    public void BuyDrill()
    {
        int cost = playerMovement.drills[Convert.ToUInt16(drillsNames[selectedDrill])].cost;
        if(selectedDrill == playerMovement.currentDrill)
        {
            Debug.Log("Already own Drill");
        }
        else if(playerInventory.getMoney()  <= cost)
        {
            Debug.Log("You Don't have enough Money");
        }
        else
        {
            playerInventory.addToMuny(-cost);
            playerMovement.currentDrill = selectedDrill;
        }

       
    }

}
