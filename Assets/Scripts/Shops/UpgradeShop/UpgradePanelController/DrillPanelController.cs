using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;


public class DrillPanelController : MonoBehaviour
{

    public Button prefab;
    public Text nameText;
    public Text infoText;
    public Text costText;
    public Text speedText;

    public PlayerInventory playerInventory;
    public playerMovement playerMovement;
    public int selectedPart;

    private Button[] buttons = new Button[10];

    public delegate void NotEnoughMoney();
    public static event NotEnoughMoney NoMoney;

    public delegate void AlreadyOwned();
    public static event AlreadyOwned Owned;

    //public delegate void ButtonsGenerated();
    //public static event ButtonsGenerated DrillButtonsGenerated;

    public void Awake()
    {
        generateButtons();
    }

    public void generateButtons()
    {
        int i = 0;
        float y = prefab.transform.position.y;
        foreach (Object drill in playerMovement.drills)
        {
            buttons[i] = Instantiate(prefab);
            buttons[i].transform.SetParent(gameObject.transform.GetChild(2));
            buttons[i].transform.position = new Vector3 (prefab.transform.position.x,y,prefab.transform.position.z);
            y += -40;
            //C# gets a bit weird with variable references. To fix it I had to use a tempInt inside the for loop.
            int tempInt = i;
            buttons[i].onClick.AddListener(delegate { displayDrillInfo(tempInt); });
            buttons[i].GetComponentInChildren<Text>().text = drill.drillName;
            i++;
        }
    }

    public void displayDrillInfo(int drill)
    {
        nameText.text =  Regex.Replace(playerMovement.drills[drill].drillName,"[A-Z]"," $0").Trim();
        infoText.text = playerMovement.drills[drill].description;
        costText.text = "$" + playerMovement.drills[drill].cost.ToString();
        if (playerMovement.drills[drill].digRate == 0)
        {
            speedText.text = "Infinity";
        }
        else
        {
            speedText.text =  Convert.ToInt16(4 / playerMovement.drills[drill].digRate).ToString() + " m/s";
        }
        selectedPart = drill;
    }

    public void BuyDrill()
    {
        int cost = playerMovement.drills[selectedPart].cost;
        if(selectedPart == playerMovement.currentDrill)
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
            playerMovement.currentDrill = selectedPart;
        }

       
    }

}
