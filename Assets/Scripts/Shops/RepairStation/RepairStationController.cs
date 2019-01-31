using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RepairStationController : MonoBehaviour
{
    public Canvas healthStationCanvas;
    public Text totalText;
    public PlayerHealth playerHealth;
    public PlayerInventory playerInventory;

    private long percentageMissingHealth;
    private long totalCost;
    void OnTriggerEnter2D(Collider2D col)
    {
        percentageMissingHealth = Convert.ToInt64((playerHealth.getMissingHealth() / playerHealth.maxHealth) * 100);
        totalCost = percentageMissingHealth * 2;
        setTotalText(percentageMissingHealth, totalCost);
        if (col.gameObject.CompareTag("Player"))
        {
            openFuelGUI();
        }
    }

    private void openFuelGUI()
    {
        healthStationCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void closeCanvas()
    {
        healthStationCanvas.enabled = false;
        Time.timeScale = 1;
    }


    public void setTotalText(long missingHealth, long totalCost)
    {
        totalText.text = "MissingHealth: " + missingHealth.ToString()  + "%" + "\n Total Cost: " + totalCost.ToString(); 
    }

    public void repairMech()
    {
        if (!playerHealth.isFullHealth())
        {

            long playerMoney = playerInventory.getMoney();
            if (playerMoney <= totalCost)
            {
                totalText.text += " \n Not Enough Money!";
            }
            else
            {
                playerInventory.addToMuny(-totalCost);
                playerHealth.replenishHealth();
                totalText.text = "MissingHealth: 0% \n ToalCost: $0 \n Thank you!";
            }
        }
    }
}
