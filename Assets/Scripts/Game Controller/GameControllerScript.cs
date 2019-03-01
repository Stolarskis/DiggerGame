using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{

    private bool isGameOver;

    public delegate void GameOver();
    public static event GameOver gameOver;

    void Awake()
    {
        isGameOver = false;
        PlayerFuel.NoFuel += enableGameOver;
        PlayerHealth.NoHealth += enableGameOver;
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            //Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene("MainScene");
        }
    }

    public void enableGameOver()
    {
        isGameOver = true;
        gameOver();
    }
}
