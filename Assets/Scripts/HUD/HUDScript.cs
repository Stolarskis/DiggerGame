using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour
{
    public GameObject RestartScreen;
    public GameObject PlayerHUD;

    void Awake()
    {
        RestartScreen = GameObject.Find("RestartScreen");
        PlayerHUD = GameObject.Find("PlayerHUD");
        disableRestartScreen();
    }
    public void enableHUD()
    {
        PlayerHUD.gameObject.SetActive(true);
    }

    public void disableHUD()
    {
        PlayerHUD.gameObject.SetActive(false);
    }

    public void enableRestartScreen()
    {
        RestartScreen.gameObject.SetActive(true);
    }
    
    public void disableRestartScreen()
    {
        RestartScreen.gameObject.SetActive(false);
    }
}
