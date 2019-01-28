using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarScript : MonoBehaviour
{
    public Transform FuelBar;
    public SpriteRenderer FuelSprite;
    private Color color;

    public void SetupFuelBar()
    {
        FuelBar = transform.Find("FillAnchor");
        FuelSprite = FuelBar.Find("FillSprite").GetComponent<SpriteRenderer>();
        color = FuelSprite.color;

    }
    
    public void setSize(float size)
    {
        FuelBar.localScale = new Vector3(1f, size);
    }
    public void enableEmergency()
    {
        StartCoroutine("emergency");
    }
    public void disableEmergency()
    {
        StopCoroutine("emergency");
        FuelSprite.color = color;
    }

    IEnumerator emergency()
    {
        while (true)
        {
            FuelSprite.color = color; 
            yield return new WaitForSeconds(0.1f);
            FuelSprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
