using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DepthScript : MonoBehaviour
{
    // Update is called once per frame
    public Text depthText;


    void Awake()
    {
        //Found incorrect text compenent
        //PlayerBalanceText = GameObject.FindObjectOfType<Text>();
        GameObject depthHUDObject = GameObject.FindGameObjectWithTag("DepthHudObject");
        if (depthHUDObject == null)
        {
            Debug.Log("unable to find depth object");
        }
        depthText= depthHUDObject.GetComponentInChildren<Text>();
        
        //PlayerBalanceText = gameObject.GetComponentInChildren<Text>();
        playerMovement.updateDepth += setText;
    }

    void OnDestroy()
    {
        playerMovement.updateDepth -= setText;    
    }

    public void setText(float depth)
    {
        int intDepth = (int)depth;
        depthText.text = intDepth.ToString() + " m";
    }
}
