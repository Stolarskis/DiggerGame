using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCanvasController : MonoBehaviour
{
    //public GameObject prefabButton;
    public GameObject drillPanel;
    public GameObject hullPanel;
    public GameObject enginePanel;
    public GameObject fuelTankPanel;

    private int numButtonsGen;


    private void Awake()
    {
        /**
        DrillPanelController.DrillButtonsGenerated += disablePrefabButton;
        EnginePanelController.EngineButtonsGenerated += disablePrefabButton;
        FuelTankPanelController.FuelTankButtonsGenerated  += disablePrefabButton;
        HullPanelController.HullButtonsGenerated  += disablePrefabButton;
        */
    }

    private void OnEnable()
    {
        //Subscribe to tabs onclick events
        TabsController.DrillTabClicked += openDrillPanel;
        TabsController.EngineTabClicked += openEnginePanel ;
        TabsController.FuelTankTabClicked += openFuelTankPanel;
        TabsController.HullTabClicked += openHullPanel; 

        //Buttons Successfully Generated
    }

    private void OnDisable()
    {
        TabsController.DrillTabClicked -= openDrillPanel;
        TabsController.EngineTabClicked -= openEnginePanel ;
        TabsController.FuelTankTabClicked -= openFuelTankPanel;
        TabsController.HullTabClicked -= openHullPanel;
    }


    public void closeAllPanels()
    {
        drillPanel.SetActive(false);
        hullPanel.SetActive(false);
        enginePanel.SetActive(false);
        fuelTankPanel.SetActive(false);
    }

    public void openAllPanels()
    {
        drillPanel.SetActive(true);
        hullPanel.SetActive(true);
        enginePanel.SetActive(true);
        fuelTankPanel.SetActive(true);
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

}
