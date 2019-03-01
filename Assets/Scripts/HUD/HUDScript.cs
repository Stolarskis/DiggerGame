using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour
{

    public Canvas playerHUD;


    void Awake()
    {
        GameControllerScript.gameOver += disableHUD;
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
