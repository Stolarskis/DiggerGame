using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarScript : MonoBehaviour
{
    public Transform FuelBar;
    public SpriteRenderer FuelSprite;
    public Image FuelImage;
    private Color color;


    void Awake()
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
            FuelImage.color = color;
            yield return new WaitForSeconds(0.1f);
            FuelImage.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
