using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCanvasController : MonoBehaviour
{

    public GameObject NotEnoughMoneyPanel;
    public GameObject AlreadyOwnedPanel;
    public GameObject NothingToSellPanel;
    public GameObject RestartScreen;
    public GameObject LoadingScreen;

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
        GameControllerScript.gameRestarted += setup;
        GameControllerScript.loadingScene += loadingScene; 
    }

    private void OnDestroy()
    {
        EnginePanelController.NoMoney -= enableNoMoneyPanel;
        FuelTankPanelController.NoMoney -= enableNoMoneyPanel;
        HullPanelController.NoMoney -= enableNoMoneyPanel;
        DrillPanelController.NoMoney -= enableNoMoneyPanel;

        EnginePanelController.Owned -= enableAlreadyOwnedPanel;
        FuelTankPanelController.Owned -= enableAlreadyOwnedPanel;
        HullPanelController.Owned  -= enableAlreadyOwnedPanel;
        DrillPanelController.Owned -= enableAlreadyOwnedPanel;

        RepairStationController.NoMoneyToRepair -= enableNoMoneyPanel;
        FuelStationController.NoMoneyToBuyGas -= enableNoMoneyPanel;
        OreProcessorController.NothingToProcess -= enableNothingToSellPanel;

        GameControllerScript.gameOver -= enableRestartScreen;
        GameControllerScript.gameRestarted -= setup;

        GameControllerScript.loadingScene -= loadingScene; 
    }

    public void setup()
    {
        RestartScreen.SetActive(false);
        LoadingScreen.SetActive(false);

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
        if (RestartScreen != null)
        {
            RestartScreen.SetActive(true);
        }
        else
        {
            Debug.Log("Restart Screen is null???");
        }

    }
    public void disableRestartScreen()
    {
        RestartScreen.SetActive(false);
    }

    public void loadingScene()
    {
        disableRestartScreen();
        LoadingScreen.SetActive(true);
    }
}
