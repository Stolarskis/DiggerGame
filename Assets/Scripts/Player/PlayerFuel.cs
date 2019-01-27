using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuel : MonoBehaviour
{
    public FuelBarScript FuelBar;
    public float maxFuel;
    public float emergencyThreshold = 0.3f;
    private float currentFuel;
    private bool isEmergency;
    

    void Start()
    {
        isEmergency = false;
        setMaxFuel(20);
        StartCoroutine("useFuel");
    }

    IEnumerator useFuel()
    {
        while (true)
        {
            if (currentFuel <= 0)
            {
                gameOver();
                StopCoroutine("useFuel"); 
            }
            else
            {
                decrementFuel();
            }

            yield return new WaitForSeconds(1);
        }

    }

    private void gameOver() { Debug.Log("Game over????"); }

    public float getFuelLevel()
    {
        return currentFuel;
    }
    public void refillFuel()
    {
        currentFuel = maxFuel;
    }

    private void decrementFuel()
    {
        currentFuel--;
        float size = currentFuel / maxFuel;
        Debug.Log("setSize");
        FuelBar.setSize(size);

        if(!isEmergency && size <= emergencyThreshold)
        {
            FuelBar.enableEmergency();
        }
        if (isEmergency && size > emergencyThreshold)
        {
            FuelBar.disableEmergency();
        }
    }

    public void setMaxFuel(float max)
    {
        maxFuel = max;
        currentFuel = maxFuel;
    }


}
