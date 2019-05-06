using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BalanceScript : MonoBehaviour
{
    // Update is called once per frame
    public Text playerBalanceText;


    void Awake()
    {
        //Found incorrect text compenent
        //PlayerBalanceText = GameObject.FindObjectOfType<Text>();
        GameObject playerBalance = GameObject.FindGameObjectWithTag("PlayerBalance");
        playerBalanceText = playerBalance.GetComponentInChildren<Text>();
        
        //PlayerBalanceText = gameObject.GetComponentInChildren<Text>();
        PlayerInventory.updateMoney += setText;
    }

    void OnDestroy()
    {
        PlayerInventory.updateMoney -= setText;    
    }

    void Start()
    {
    }

    public void setText(long money)
    {
            playerBalanceText.text = "$" + money.ToString();
    }
}
