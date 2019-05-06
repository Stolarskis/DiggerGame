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

    public delegate void inUI();
    public static event inUI playerInUI;

    public delegate void outOfUI();
    public static event outOfUI playerOutOfUI;


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
            openUpgradeShopGui();
            playerInUI();
        }
    }
    
    private void openUpgradeShopGui()
    {
        upgradeCanvas.enabled = true;

        //This used to work but I can't use it because if time stops, then animations cease to play.
        //Time.timeScale = 0;
    }

    public void closeCanvas()
    {
        upgradeCanvas.enabled = false;
        playerOutOfUI();
       //Time.timeScale = 1;
    }


    
}
