using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInventory : MonoBehaviour
{
    private Hashtable inventory = new Hashtable();
    private Hashtable ores = new Hashtable()
    {
        {"iron", 1},
        {"silver",2},
        {"gold",3},
        {"platinum",4}
    };


    private void Start()
    {
        inventory = new Hashtable();
    }

    void FixedUpdate()
    {
        //Debug.Log(inventory["iron"]);
    }
    public void addToInventory(TileBase tile)
    {
        if (tile == null)
        {
            
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
