using System.Collections;
using System.Collections.Generic;
using System; 
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerMovement : MonoBehaviour
{
    public playerController controller;
    public Tilemap map;
    public float digRate;
    public float digDistance= 1.2f;
    
    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private float nextDig;
    private bool jump = false;
    public bool isDig = false;


    private void Start()
    {
        //StartCoroutine(dig());
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButton("Jump"))
        {
            jump = true;
        }
    }
    void FixedUpdate()
    {
        if (!isDig)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
            string direction = checkPlayerDigState();
            if (direction != "")
            {
                startDig(direction);
            }
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
    
    //TODO: Need a better way of detecting if the player is in the correct state to dig. That is resting and touching a block. ie player is triyng to move but can't because of a tile.
    //Checks if player is in a state that allows for digging. (Not in the air, pressed up against a block)
    string checkPlayerDigState()
    {
        int x = (int)Math.Round(gameObject.transform.position.x);
        int y = (int)Math.Round(gameObject.transform.position.y);
        int z = (int)Math.Round(gameObject.transform.position.z);
        
        if (map.HasTile(new Vector3Int(x - 1, y, z)))
        {
            Debug.Log("pressing against left tile");
        }
        if (map.HasTile(new Vector3Int(x + 1, y, z)))
        {
            Debug.Log("pressing against right tile");
        }
        if (map.HasTile(new Vector3Int(x, y-1, z)))
        {
            Debug.Log("pressing against bottom tile");
        }



        if (controller.m_Grounded)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                return "down";
            }
            else if (Input.GetAxisRaw("Horizontal") > 0 && map.HasTile(new Vector3Int(x + 1, y, z)))
            {
                return "right";
            }
            else if (Input.GetAxisRaw("Horizontal") < 0 &&  map.HasTile(new Vector3Int(x - 1, y, z)))
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
            yield return new WaitForSeconds(digRate);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - digDistance , gameObject.transform.position.z);
            checkTileAndDelete();
        }
        else if (direction == "right")
        {
            yield return new WaitForSeconds(digRate);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + digDistance, gameObject.transform.position.y, gameObject.transform.position.z);
            checkTileAndDelete();
        }
        else if (direction == "left")
        {
                yield return new WaitForSeconds(digRate);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - digDistance, gameObject.transform.position.y, gameObject.transform.position.z);
                checkTileAndDelete();
        }
        isDig = false;
        yield return null;
    }
    void checkTileAndDelete()
    {
        int x = (int)Math.Round(gameObject.transform.position.x);
        int y = (int)Math.Round(gameObject.transform.position.y);
        int z = (int)Math.Round(gameObject.transform.position.z);
        Vector3Int checkPosition = new Vector3Int(x, y, z);
        if (map.HasTile(checkPosition))
        {
            map.SetTile(checkPosition, null);
        } 
    }
}
