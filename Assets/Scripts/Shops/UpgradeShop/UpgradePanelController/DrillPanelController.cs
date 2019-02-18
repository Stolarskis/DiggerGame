using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DrillPanelController : MonoBehaviour
{

    public Button prefab;
    public Text DrillNameText;
    public Text DrillInfoText;
    public Text DrillCostText;
    public Text DrillDigRateText;

    public PlayerInventory playerInventory;
    public playerMovement playerMovement;
    public int selectedDrill;

    private Button[] DrillButtons = new Button[10];

    public delegate void NotEnoughMoney();
    public static event NotEnoughMoney NoMoney;

    public delegate void AlreadyOwned();
    public static event AlreadyOwned Owned;

    //public delegate void ButtonsGenerated();
    //public static event ButtonsGenerated DrillButtonsGenerated;

    public void Awake()
    {
        generateButtons();
        //DrillButtonsGenerated();
    }

    public void generateButtons()
    {
        int i = 0;
        float y = prefab.transform.position.y;
        foreach (DrillObject drill in playerMovement.drills)
        {
            DrillButtons[i] = Instantiate(prefab);
            DrillButtons[i].transform.SetParent(gameObject.transform.GetChild(2));
            DrillButtons[i].transform.position = new Vector3 (prefab.transform.position.x,y,prefab.transform.position.z);
            y += -50;
            //C# gets a bit weird with variable references. To fix it I had to use a tempInt inside the for loop.
            int tempInt = i;
            DrillButtons[i].onClick.AddListener(delegate { displayDrillInfo(tempInt); });
            DrillButtons[i].GetComponentInChildren<Text>().text = drill.name;
            i++;
        }
    }

    public void displayDrillInfo(int drill)
    {
        DrillNameText.text = "Type: " + playerMovement.drills[drill].name;
        DrillInfoText.text = playerMovement.drills[drill].description;
        DrillCostText.text = "$" + playerMovement.drills[drill].cost.ToString();
        DrillDigRateText.text = "DigRate: " + playerMovement.drills[drill].digRate.ToString();
        selectedDrill = drill;
    }

    public void BuyDrill()
    {
        int cost = playerMovement.drills[selectedDrill].cost;
        if(selectedDrill == playerMovement.currentDrill)
        {
            Owned();
        }
        else if(playerInventory.getMoney()  <= cost)
        {
            NoMoney();
        }
        else
        {
            playerInventory.addToMuny(-cost);
            playerMovement.currentDrill = selectedDrill;
        }

       
    }

}
