using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceScript : MonoBehaviour
{
    // Update is called once per frame
    public TextMesh PlayerBalanceText;

    private long playerBalance = 0;
    
    public void Deposit(long amount)
    {
        //StartCoroutine(DepositHelper(amount));
        playerBalance += amount;
        displayScore();
    }

    IEnumerator DepositHelper(long count)
    {
        count = count / 10;
        for (int i = 0; i < count; i++)
        {
            playerBalance+=10;
            displayScore();
            yield return new WaitForSeconds(1/count);
        }
    }

    public bool Withdrawl(long amount)
    {
        if (amount > playerBalance)
        {
            return false;
        }
        else
        {
            playerBalance -= amount;
            displayScore();
            return true;
        }
    }

    public void incrementScore()
    {
        playerBalance += 1;
        displayScore();
    }

    public long getBalance()
    {
        return playerBalance;
    }
    private void displayScore()
    {
        PlayerBalanceText.text = "$" + playerBalance.ToString();
    }
}
