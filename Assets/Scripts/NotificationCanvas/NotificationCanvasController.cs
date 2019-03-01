using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCanvasController : MonoBehaviour
{

    public GameObject NotEnoughMoneyPanel;
    public GameObject AlreadyOwnedPanel;
    public GameObject NothingToSellPanel;
    public GameObject RestartScreen;

    //Subscribe to panel notifications
    public void OnEnable()
    {
        EnginePanelController.NoMoney += enableNoMoneyPanel;
        FuelTankPanelController.NoMoney += enableNoMoneyPanel;
        HullPanelController.NoMoney += enableNoMoneyPanel;
        DrillPanelController.NoMoney += enableNoMoneyPanel;

        EnginePanelController.Owned += enableAlreadyOwnedPanel;
        FuelTankPanelController.Owned += enableAlreadyOwnedPanel;
        HullPanelController.Owned+= enableAlreadyOwnedPanel;
        DrillPanelController.Owned += enableAlreadyOwnedPanel;

        RepairStationController.NoMoneyToRepair += enableNoMoneyPanel;
        FuelStationController.NoMoneyToBuyGas+= enableNoMoneyPanel;
        OreProcessorController.NothingToProcess += enableNothingToSellPanel;

        GameControllerScript.gameOver += enableRestartScreen; 
    }

    public void closeNotificationPanel()
    {
        NotEnoughMoneyPanel.SetActive(false);
        AlreadyOwnedPanel.SetActive(false);
        NothingToSellPanel.SetActive(false);
    }

    public void enableNoMoneyPanel()
    { 
        NotEnoughMoneyPanel.SetActive(true);
    }
    
    public void enableAlreadyOwnedPanel()
    {
        AlreadyOwnedPanel.SetActive(true);
    }

    public void enableNothingToSellPanel()
    {
        NothingToSellPanel.SetActive(true);
    }

    public void enableRestartScreen()
    {
        RestartScreen.SetActive(true);
    }
    public void disableRestartScreen()
    {
        RestartScreen.SetActive(false);
    }
}
