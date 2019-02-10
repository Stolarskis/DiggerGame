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
    private float totalCost;

    public delegate void NoMoney();
    public static event NoMoney NoMoneyToRepair;


    void OnTriggerEnter2D(Collider2D col)
    {
        percentageMissingHealth = Convert.ToInt64((playerHealth.getMissingHealth() / playerHealth.hull[playerHealth.selectedHull].maxHealth) * 100);
        totalCost = playerHealth.getMissingHealth();
        setTotalText(percentageMissingHealth, Convert.ToInt16(totalCost));
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


    public void setTotalText(long missingHealth, float totalCost)
    {
        totalText.text = "MissingHealth: " + missingHealth.ToString()  + "%" + "\nTotal Cost: " + totalCost.ToString(); 
    }

    public void repairMech()
    {
        if (!playerHealth.isFullHealth())
        {

            long playerMoney = playerInventory.getMoney();
            if (playerMoney <= totalCost)
            {
                NoMoneyToRepair();
            }
            else
            {
                playerInventory.addToMuny(-Convert.ToInt64(totalCost));
                playerHealth.replenishHealth();
                totalText.text = "MissingHealth: 0% \n ToalCost: $0 \n Thank you!";
            }
        }
    }
}
