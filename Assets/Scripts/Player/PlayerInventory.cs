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
    private long muny;
    private Hashtable inventory = new Hashtable();
    //Value = how much inventory space the item takes up
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

    private void Awake()
    {
        inventory = new Hashtable();
        currentInventorySpace = 20;
        muny = 0;
    }

    private void Start()
    {
        addToMuny(100000); 
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

    public void decrementInventory(string ore)
    {
        if (!inventory.Contains(ore)){
            return;
        }
        if ((int)inventory[ore] > 1)
        {
            inventory[ore] = (int)inventory[ore] - 1;
        }
        else 
        {
            inventory.Remove(ore);
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
        muny += value;
        if (muny < 0)
        {
            muny = 0;
        }
        //TODO: need to add a event here
        balanceHUD.setText(muny);
    }
    public long getMoney()
    {
        return muny;
    }





}
