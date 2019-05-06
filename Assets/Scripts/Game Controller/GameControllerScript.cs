using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{

    private bool isGameOver;

    public delegate void GameOver();
    public static event GameOver gameOver;

    public delegate void GameRestarted();
    public static event GameRestarted gameRestarted;

    public delegate void LoadingScene();
    public static event LoadingScene loadingScene;

    public delegate void inUI();
    public static event inUI playerInUI;

    public delegate void outOfUI();
    public static event outOfUI playerOutOfUI;


    void Awake()
    {
        isGameOver = false;
        //Subscribe to end game states.
        PlayerFuel.NoFuel += enableGameOver;
        PlayerHealth.NoHealth += enableGameOver;

        UpgradeStationController.playerInUI += disablePlayerMovement; 
        UpgradeStationController.playerOutOfUI+= enablePlayerMovement; 

        //Subscribe all UI events to restrict the player movements
        gameRestarted();

    }

    private void OnDisable()
    {
        PlayerFuel.NoFuel -= enableGameOver;
        PlayerHealth.NoHealth -= enableGameOver;

        UpgradeStationController.playerInUI -= disablePlayerMovement; 
        UpgradeStationController.playerOutOfUI-= enablePlayerMovement; 

        
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            loadingScene();
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }
    }

    public void enableGameOver()
    {
        isGameOver = true;
        gameOver();
    }

    //Signals player movement script to prevent player movement.
    public void enablePlayerMovement()
    {
        playerOutOfUI();
    }

    //Signals player movement script to enable player movement.
    public void disablePlayerMovement()
    {
        playerInUI();
    }
}
