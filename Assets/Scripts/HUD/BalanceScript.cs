using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BalanceScript : MonoBehaviour
{
    // Update is called once per frame
    public Text PlayerBalanceText;


    void Awake()
    {
        //Found incorrect text compenent
        //PlayerBalanceText = GameObject.FindObjectOfType<Text>();
        PlayerBalanceText = gameObject.GetComponentInChildren<Text>();
    }

    void Start()
    {
        PlayerBalanceText.text = "$0";   
    }

    public void setText(long money)
    {
        if (money == 0)
        {
            PlayerBalanceText.text = "No Money";
        }
        else
        {
            if (PlayerBalanceText == null)
            {
                Debug.Log("PlayerBalance is null");
            }
            PlayerBalanceText.text = "$" + money.ToString();
        }
    }
}
