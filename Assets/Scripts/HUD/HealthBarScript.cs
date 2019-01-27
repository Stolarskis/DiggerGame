using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Transform HealthBar;
    public SpriteRenderer HealthSprite;


    public int maxHealth;
    private int currentHealth;
    private bool isEmergency;
    private Color color;   

    private void Start()
    {
        isEmergency = false;
        HealthBar = transform.Find("FillAnchor");
        HealthSprite = HealthBar.Find("FillSprite").GetComponent<SpriteRenderer>();
        color = HealthSprite.color;
    }

    public void changeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth <= 0)
        {
            Debug.Log("DEAD!!!");
        }
        int size = currentHealth / maxHealth;
        setSize(size);

         if(!isEmergency && size <= 0.2f)
        {
            enableEmergency();
        }
        if (isEmergency && size > 0.2f)
        {
            disableEmergency();
        }
        setSize(size);
    }
    
    private void setSize(float size)
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
    }
    public void refillHealth()
    {
        
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