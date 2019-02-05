using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public HUDScript HUD;
    private bool isGameOver;

    void Awake()
    {
        isGameOver = false;
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void enableGameOver()
    {
        HUD.disableHUD();
        HUD.enableRestartScreen();
        isGameOver = true;     
    }
}
