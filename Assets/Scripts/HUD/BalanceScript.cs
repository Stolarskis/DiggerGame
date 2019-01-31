using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceScript : MonoBehaviour
{
    // Update is called once per frame
    public TextMesh PlayerBalanceText;

    public void setText(long money)
    {
        if (money == 0)
        {
            PlayerBalanceText.text = "No Money";
        }
        else
        {
        PlayerBalanceText.text = "$" + money.ToString();
        }
    }
}
