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
        drillPanel.SetActive(true);
    }

    
}
