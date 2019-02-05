using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStationController : MonoBehaviour
{
    public PlayerInventory inventory;
    public PlayerHealth health;
    public PlayerFuel fuel;
    public playerMovement movement;

    public Canvas upgradeCanvas;

    public GameObject drillPanel;
    public GameObject hullPanel;
    public GameObject enginePanel;
    public GameObject fuelTankPanel;

    public GameObject notificationPanel;

    //Notification subscribers
    private void OnEnable()
    {
        EnginePanelController.NoMoney += enableNotification;
    }

    private void OnDisable()
    {
        
        EnginePanelController.NoMoney -= disableNotification;
    }

    private void Awake()
    {
        closeAllPanels();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            openFuelGUI();
        }
    }
    
    private void openFuelGUI()
    {
        upgradeCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void closeCanvas()
    {
        upgradeCanvas.enabled = false;
        Time.timeScale = 1;
    }


    public void closeAllPanels()
    {
        drillPanel.SetActive(false);
        hullPanel.SetActive(false);
        enginePanel.SetActive(false);
        fuelTankPanel.SetActive(false);
    }

    public void openDrillPanel()
    {
        closeAllPanels();
        drillPanel.SetActive(true);
    }

    public void openHullPanel()
    {
        closeAllPanels();
        hullPanel.SetActive(true);
    }

    public void openEnginePanel()
    {
        closeAllPanels();
        enginePanel.SetActive(true);
    }

    public void openFuelTankPanel()
    {
        closeAllPanels();
        fuelTankPanel.SetActive(true);
    }

    public void enableNotification()
    {
        notificationPanel.SetActive(true);
    }
    public void disableNotification()
    {
        notificationPanel.SetActive(false);
    }
    
}
