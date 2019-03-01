using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Transform HealthBar;
    public SpriteRenderer HealthSprite;
    public Image HealthImage;

    private bool isEmergency;
    private Color color;

    private void Awake()
    {
        color = HealthSprite.color;
    }

    public void setupHealthBar()
    {
        
        isEmergency = false;
        HealthBar = transform.Find("FillAnchor");
        HealthSprite = HealthBar.Find("FillSprite").GetComponent<SpriteRenderer>();
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
            HealthImage.color = color;
            yield return new WaitForSeconds(0.1f);
            HealthImage.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    
}