using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class HullPanelController : MonoBehaviour
{
    public Button prefab;
    public Text HullNameText;
    public Text HullCostText;
    public Text MaxHullText;

    public float buttonSpacingY;
    public PlayerInventory playerInventory;
    public PlayerHealth playerHealth;
    public int selectedHull;

    private Button[] hullButtons = new Button[10];
        
    //Events
    public delegate void NotEnoughMoney();
    public static event NotEnoughMoney NoMoney;

    public delegate void AlreadyOwned();
    public static event AlreadyOwned Owned;

    public delegate void ButtonsGenerated();
    public static event ButtonsGenerated HullButtonsGenerated;

    public void Awake()
    {
        buttonSpacingY = -50f;
        generateButtons();
        //HullButtonsGenerated();
    }

    public void generateButtons()
    {
        int i = 0;
        float y = prefab.transform.position.y;
        foreach (HullObject hull in playerHealth.hull)
        {
            hullButtons[i] = Instantiate(prefab);
            hullButtons[i].transform.SetParent(gameObject.transform.GetChild(2));
            hullButtons[i].transform.position = new Vector3 (prefab.transform.position.x,y,prefab.transform.position.z);
            y += -50;
            //C# gets a bit weird with variable references. To fix it I had to use a tempInt inside the for loop.
            int tempInt = i;
            hullButtons[i].onClick.AddListener(delegate { displayHull(tempInt); });
            hullButtons[i].GetComponentInChildren<Text>().text = hull.name;
            i++;
        }
    }

    public void displayHull(int hull)
    {
        HullNameText.text = Regex.Replace(playerHealth.hull[hull].name,"[A-Z]"," $0").Trim();
        HullCostText.text = "$" + playerHealth.hull[hull].cost.ToString();
        MaxHullText.text = playerHealth.hull[hull].maxHealth.ToString() + " Health";
        selectedHull = hull;
    }

    public void BuyHull()
    {
        Debug.Log("Buy hull button pressed");
        Debug.Log(selectedHull);
        long cost = playerHealth.hull[selectedHull].cost;
        if(selectedHull == playerHealth.selectedHull)
        {
            Owned();
        }
        else if(playerInventory.getMoney() < cost)
        {
            NoMoney();
        }
        else
        {
            playerInventory.addToMuny(-cost);
            playerHealth.selectedHull = selectedHull;
            playerHealth.replenishHealth();
        }

    }

}