using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScript : MonoBehaviour
{
    public TextMesh restartText;

    public void displayRestartText()
    {
        restartText.text = "Game Over \n \n \n Press R to Restart";
    }
    public void removeRestartText()
    {
        restartText.text = "";
    }
}
