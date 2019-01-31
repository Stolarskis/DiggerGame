﻿using System.Collections;
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
    private Hashtable orePrices = new Hashtable()
    {
        {"iron", 10},
        {"copper",20},
        {"gold",50},
        {"platinum",100}
    };
    private long total;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Something entered the collider");
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
        inventoryScript.addToMuny(total);
        inventoryScript.clearInventory();
        playerInventory.Clear();
        total = 0;
        playerInventoryText.text = "Total: 0";
    }

    public void displayPlayerInventory()
    {
        string invenText = "";
        long totalCost = 0;
        foreach (DictionaryEntry s in playerInventory)
        {
            invenText +=s.Key.ToString() + ": " +  s.Value.ToString() + "\n";
            totalCost += calcTotal(s);
        }
        invenText += "\n" + "Total:  " + totalCost;
        playerInventoryText.text = invenText;
        total = totalCost;
    }
    public long calcTotal(DictionaryEntry ore)
    {
        if (!orePrices.ContainsKey(ore.Key.ToString()))
        {
            return 0;
        }
        else
        {
            return Convert.ToInt64(ore.Value.ToString()) * Convert.ToInt64(orePrices[ore.Key.ToString()]);
        }
    }
}