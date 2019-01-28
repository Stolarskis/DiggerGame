﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInventory : MonoBehaviour
{
    public int maxInventorySpace;

    private int currentInventorySpace;
    private long muny;
    private Hashtable inventory = new Hashtable();
    //Value = how much inventory space the item takes up
    private Hashtable ores = new Hashtable()
    {
        {"iron", 4},
        {"copper",4},
        {"gold",2},
        {"platinum",2}
    };


    private void Awake()
    {
        inventory = new Hashtable();
        
        inventorySpace = 20;
        muny = 0;
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
    public Hashtable clearInventory()
    {
        Hashtable temp = inventory;
        inventory.Clear();
        return temp;
    }


}
