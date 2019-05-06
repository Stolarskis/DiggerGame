using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour
{

    public Canvas playerHUD;

    public void Awake()
    {
        GameControllerScript.gameRestarted += setup;
        GameControllerScript.gameOver += disableHUD;
    }
    public void OnDestroy()
    {
        GameControllerScript.gameRestarted -= setup;
        GameControllerScript.gameOver -= disableHUD;

    }

    public void setup()
    {
        GameObject canvasObject = GameObject.FindGameObjectWithTag("PlayerHUD");
        playerHUD = canvasObject.GetComponent<Canvas>();
    }
    public void enableHUD()
    {
        playerHUD.enabled = true;
    }

    public void disableHUD()
    {
        playerHUD.enabled = false;
    }
}
