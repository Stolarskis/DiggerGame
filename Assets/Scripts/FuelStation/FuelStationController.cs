using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelStationController : MonoBehaviour
{

    public Canvas fuelStationCanvas;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Something entered the collider");
        if (col.gameObject.CompareTag("Player"))
        {
            openFuelGUI();
        }
    }

    private void openFuelGUI()
    {
        fuelStationCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void closeCanvas()
    {
        fuelStationCanvas.enabled = false;
        Time.timeScale = 1;
    }
}
