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

    public Canvas notificationCanvas;
    public NotificationCanvasController notificationController;

    public Canvas upgradeCanvas;
    public UpgradeCanvasController UpgradeController;

    //Notification subscribers
    private void OnEnable()
    {
       //EnginePanelController.NoMoney += enableNotification;
    }

    private void OnDisable()
    {
        //EnginePanelController.NoMoney -= disableNotification;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //UpgradeController.closeAllPanels();
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


    
}
