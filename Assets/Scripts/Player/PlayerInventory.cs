using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInventory : MonoBehaviour
{
    public int maxInventorySpace;
    public BalanceScript balanceHUD;

    //TODO: Inventory space is check for 
    private int currentInventorySpace;
    private long playerMoney;
    private Hashtable inventory = new Hashtable();
    //Value = how much inventory space the item takes up
    //Ores hashtable = name of ore : [value of ore, mass (how much inventory space it takes up)]
    public Hashtable ores = new Hashtable()
    {
        {"iron", new int[]{25,10} },
        {"copper", new int[]{25,20} },
        {"siler" , new int[]{50, 50}},
        {"gold", new int[]{75, 100} },
        {"platinum", new int[]{100, 250} },
        //NEED MORE ORES HERE

        {"emerald",new int []{125, 2000} },
        {"ruby",new int[]{150, 5000 } },
        //Sappire maybe?
        {"diamond", new int[] {175, 20000} },


    };
    //TODO Implement Inventoryspace objects + the logic for inventory space
    public delegate void UpdateMoney(long money);
    public static event UpdateMoney updateMoney;

    private void Awake()
    {
        inventory = new Hashtable();
        currentInventorySpace = 20;
        playerMoney = 0;
    }

    private void Start()
    {
        addToMuny(10000000000); 
    }

    public void addToInventory(TileBase tile)
    {
        if (tile == null)
        {
            return;
        }
        else
        {
            if (ores.ContainsKey(tile.name))
            {
                if (inventory.Contains(tile.name))
                {
                    inventory[tile.name] = (int)inventory[tile.name] + 1;
                }
                else
                {
                    inventory[tile.name] = 1;
                }
            }
        }
    }

 
    public void clearInventory()
    {
        inventory.Clear();
    }
    public Hashtable getInventory()
    {
        return inventory;
    }

    public void addToMuny(long value)
    {
        playerMoney += value;
        if (playerMoney < 0)
        {
            playerMoney = 0;
        }
        updateMoney(playerMoney);
    }
    public long getMoney()
    {
        return playerMoney;
    }





}
