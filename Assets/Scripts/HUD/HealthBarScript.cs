using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Transform HealthBar;
    public SpriteRenderer HealthSprite;

    private bool isEmergency;
    private Color color;   

   public void setupHealthBar()
    {
        
        isEmergency = false;
        HealthBar = transform.Find("FillAnchor");
        HealthSprite = HealthBar.Find("FillSprite").GetComponent<SpriteRenderer>();
        color = HealthSprite.color;
    }
    
    public void setSize(float size)
    {
        HealthBar.localScale = new Vector3(1f, size);
    }
    public void enableEmergency()
    {
        StartCoroutine("emergency");
    }
    public void disableEmergency()
    {
        StopCoroutine("emergency");
        HealthSprite.color = color;
    }

    IEnumerator emergency()
    {
        while (true)
        {
            HealthSprite.color = color; 
            yield return new WaitForSeconds(0.1f);
            HealthSprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    
}