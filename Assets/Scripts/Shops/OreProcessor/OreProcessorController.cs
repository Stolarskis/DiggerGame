using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OreProcessorController : MonoBehaviour
{
    public Canvas oreProcessorCanvas;
    public PlayerInventory inventoryScript;
    public Text playerInventoryText;

    private Hashtable playerInventory;
    
   private long total;

   public delegate void NothingToSell();
   public static event NothingToSell NothingToProcess;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            total = 0;
            openOreGUI();
            playerInventory = inventoryScript.getInventory();
            displayPlayerInventory();
        }
    }

    private void openOreGUI()
    {
        oreProcessorCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void closeCanvas()
    {
        oreProcessorCanvas.enabled = false;
        Time.timeScale = 1;
    }

    public void sellAll()
    {
        if(total == 0)
        {
            NothingToProcess();   
        }
        else
        {
            inventoryScript.addToMuny(total);
            inventoryScript.clearInventory();
            playerInventory.Clear();
            total = 0;
            playerInventoryText.text = "Total: 0";
        }
    }

    public void displayPlayerInventory()
    {
        string invenText = "";
        string totalText = "";
        long totalCost = 0;
        foreach (DictionaryEntry s in playerInventory)
        {
            invenText +=s.Key.ToString() + ": " +  s.Value.ToString() + "\n";
            totalCost += calcTotal(s);
        }
        totalText = "Total:  " + totalCost + "\n \n Player Inventory \n --------------------- \n";
        totalText += invenText; 
        playerInventoryText.text = totalText;
        total = totalCost;
    }
    public long calcTotal(DictionaryEntry ore)
    {
        if (!inventoryScript.ores.ContainsKey(ore.Key.ToString()))
        {
            return 0;
        }
        else
        {
            int[] a = (int[])inventoryScript.ores[ore.Key.ToString()];
            return Convert.ToInt64(ore.Value.ToString()) * Convert.ToInt64(a[1]);
        }
    }
}
