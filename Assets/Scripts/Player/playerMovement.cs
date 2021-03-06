﻿using System.Collections;
using System.Collections.Generic;
using System; 
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerMovement : MonoBehaviour
{
    public playerController controller;
    public PlayerInventory inventory;
    public PlayerHealth health;
    public Tilemap map;
    public float digRate;
    public float digDistance= 1.2f;
    public Transform isTouchingCheck;
    public Transform playerTransform;
    //Invunerable tiles
    public List<String> iTiles = new List<String>();
    
    public float runSpeed = 40f;
    public float flySpeed = 100f;
    private float horizontalMove;
    private float verticalMove;
    private float nextDig;
    private float isTouchingCheckRadius = 0.1f;
    private float fallingSum;

    private long depth = 0;

    private bool isDig;
    private bool movementDisabled;

    public DrillObject[] drills;
    public int currentDrill = 0;

    public EngineObject[] engines;
    public int currentEngine = 0;

    public delegate void UpdateDepth(float depth);
    public static event UpdateDepth updateDepth;

    private void Awake()
    {
        //General Setup (Might put this into a function)
        iTiles.Add("concrete");
        iTiles.Add("boulder");
        
        fallingSum = 0;
        horizontalMove = 0f;
        verticalMove = 0f;
        isDig = false;
        movementDisabled = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        GameControllerScript.gameOver += disableMovement;
        GameControllerScript.playerInUI += disableMovement;
        GameControllerScript.playerOutOfUI += enableMovement;
    }

    void OnDestroy()
    {
        GameControllerScript.gameOver -= disableMovement;
        GameControllerScript. playerInUI -= disableMovement;
        GameControllerScript.playerOutOfUI -= enableMovement;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * engines[currentEngine].runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * engines[currentEngine].flySpeed;

    }
    void FixedUpdate()
    {


        applyFallDamage();
        if (!isDig && !movementDisabled)
        {
                controller.Move(horizontalMove * Time.fixedDeltaTime, false);
                controller.Fly(verticalMove * Time.fixedDeltaTime);
                string direction = checkPlayerDigState();
                if (direction != "")
                {
                    startDig(direction);
                }

            updateDepth?.Invoke(playerTransform.position.y * 12);
        }
    }

    //StartDig ensures that only one coroutine of dig functions at a time. If multiple coroutines spin up at the same time
    //Player gets teleported around like a mad man.
    void startDig(string direction)
    {
        StopAllCoroutines();
        isDig = true;
        StartCoroutine(dig(direction));
    }
    
   string checkPlayerDigState()
    {
        int x = (int)Math.Round(gameObject.transform.position.x);
        int y = (int)Math.Round(gameObject.transform.position.y);
        int z = (int)Math.Round(gameObject.transform.position.z);
        
        if (controller.m_Grounded)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                return "down";
            }
            else if (Input.GetAxisRaw("Horizontal") > 0 && map.HasTile(new Vector3Int(x + 1, y, z)) && isTouching())
            {
                return "right";
            }
            else if (Input.GetAxisRaw("Horizontal") < 0 &&  map.HasTile(new Vector3Int(x - 1, y, z)) && isTouching())
            {
                return "left";
            }
        }
        return "";
    }
    IEnumerator dig(string direction)
    {
        //Wait for specified dig rate
        if (direction == "down")
        {
            yield return new WaitForSeconds(drills[currentDrill].digRate);
            Vector3Int position = new Vector3Int((int)Math.Round(gameObject.transform.position.x), (int)Math.Round(gameObject.transform.position.y - digDistance), 0);
            TileBase tile = map.GetTile(position);
            deleteTileAndMove(position, tile);
        }
        if (direction == "right")
        {
            yield return new WaitForSeconds(drills[currentDrill].digRate);
;
            Vector3Int position = new Vector3Int((int)Math.Round(gameObject.transform.position.x + digDistance), (int)Math.Round(gameObject.transform.position.y), (int)Math.Round(gameObject.transform.position.z));
            TileBase tile = map.GetTile(position);
            deleteTileAndMove(position, tile);
        }
        if (direction == "left")
        {
            yield return new WaitForSeconds(drills[currentDrill].digRate);
;
            Vector3Int position = new Vector3Int((int)Math.Round(gameObject.transform.position.x - digDistance), (int)Math.Round(gameObject.transform.position.y), (int)Math.Round(gameObject.transform.position.z));
            TileBase tile = map.GetTile(position);
            deleteTileAndMove(position, tile);
        }
        isDig = false;
        yield return null;
    }
    void deleteTileAndMove(Vector3Int position, TileBase tile)
    {
        if (tile != null)
        {
            map.SetTile(position, null);
            gameObject.transform.position = position;
            inventory.addToInventory(tile);
        }
    }
    private bool isTouching()
    {
        bool isTouching = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(isTouchingCheck.position, isTouchingCheckRadius, controller.m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isTouching = true;
            }
        }
        return isTouching;
    }
    private void applyFallDamage()
    {
        if (controller.m_Grounded)
        {
            if (fallingSum <= -25)
            {
                if (fallingSum <= -50)
                {
                    health.changeHealth(-45f);
                }
                else
                {
                    health.changeHealth(fallingSum);
                }
            }
            fallingSum = 0;
        }
        else
        {
            fallingSum = controller.m_Rigidbody2D.velocity[1];
        }
    }

private void disableMovement()
{
     movementDisabled = true; 
        controller.m_Rigidbody2D.velocity = Vector2.zero;
}

//Disable player movement flag and reset inertia to prevent player from flying off the screen.
 private void enableMovement()
{
     movementDisabled = false;
        controller.m_Rigidbody2D.AddForce(Vector2.zero);
}

}
