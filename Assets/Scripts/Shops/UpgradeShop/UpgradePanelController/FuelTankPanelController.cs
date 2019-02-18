using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FuelTankPanelController: MonoBehaviour
{

    public Button prefab;
    public Text FuelTankNameText;
    public Text FuelTankCostText;
    public Text MaxFuelText;

    public PlayerInventory playerInventory;
    public PlayerFuel playerFuel;
    public int selectedFuelTank;

    private Button[] fuelTankButtons = new Button[10];

    public delegate void NotEnoughMoney();
    public static event NotEnoughMoney NoMoney;

    public delegate void AlreadyOwned();
    public static event AlreadyOwned Owned;

    //public delegate void ButtonsGenerated();
    //public static event ButtonsGenerated FuelTankButtonsGenerated;

    private void Awake()
    {
        generateButtons();
    }

    public void generateButtons()
    {
        int i = 0;
        float y = prefab.transform.position.y;
        foreach (FuelTankObject fuelTank in playerFuel.fuelTanks)
        {
            fuelTankButtons[i] = Instantiate(prefab);
            fuelTankButtons[i].transform.SetParent(gameObject.transform.GetChild(2));
            fuelTankButtons[i].transform.position = new Vector3 (prefab.transform.position.x,y,prefab.transform.position.z);
            y += -50;
            //C# gets a bit weird with variable references. To fix it I had to use a tempInt inside the for loop.
            int tempInt = i;
            fuelTankButtons[i].onClick.AddListener(delegate { displayFuelTank(tempInt); });
            fuelTankButtons[i].GetComponentInChildren<Text>().text = fuelTank.name;
            i++;
        }
    }

    public void displayFuelTank(int fuelTank)
    {
        FuelTankNameText.text =  playerFuel.fuelTanks[fuelTank].name;
        FuelTankCostText.text = "$" + playerFuel.fuelTanks[fuelTank].cost.ToString();
        MaxFuelText.text = "Fuel Capacity: " + playerFuel.fuelTanks[fuelTank].maxFuel.ToString();
        selectedFuelTank = fuelTank;
    }

    public void BuyFueltank()
    {
        int cost = playerFuel.fuelTanks[selectedFuelTank].cost;
        if(selectedFuelTank == playerFuel.selectedFuelTank)
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
            playerFuel.selectedFuelTank = selectedFuelTank;
            playerFuel.refillFuel();
        }

    }

}