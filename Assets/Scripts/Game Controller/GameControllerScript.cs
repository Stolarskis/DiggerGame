using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public GameObject HUD;

    void Start()
    {
    }


    public void enableGameOver()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
