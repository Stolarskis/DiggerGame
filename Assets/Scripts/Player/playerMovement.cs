using System.Collections;
using System.Collections.Generic;
using System; 
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerMovement : MonoBehaviour
{
    public playerController controller;
    public PlayerInventory inventory; 
    public Tilemap map;
    public float digRate;
    public float digDistance= 1.2f;
    
    public float runSpeed = 40f;
    public float flySpeed = 100f;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private float nextDig;
    private bool isDig = false;
    private float topAngle;
    private float sideAngle;

    private void Start()
    {
        Vector2 size = GetComponent<BoxCollider2D>().size;
        size = Vector2.Scale(size, (Vector2)transform.localScale);
        topAngle = Mathf.Atan(size.x / size.y) * Mathf.Rad2Deg;
        sideAngle = 90.0f - topAngle;
        Debug.Log(topAngle + ", " + sideAngle);
    }
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * flySpeed;

    }
    void FixedUpdate()
    {
        if (!isDig)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
            controller.Fly(verticalMove * Time.fixedDeltaTime);
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
            Vector3Int position = new Vector3Int((int)Math.Round(gameObject.transform.position.x), (int)Math.Round(gameObject.transform.position.y - digDistance), 0);
            TileBase tile = map.GetTile(position);
            deleteTileAndMove(position, tile);
        }
        if (direction == "right")
        {
            yield return new WaitForSeconds(digRate);
            Vector3Int position = new Vector3Int((int)Math.Round(gameObject.transform.position.x + digDistance), (int)Math.Round(gameObject.transform.position.y), (int)Math.Round(gameObject.transform.position.z));
            TileBase tile = map.GetTile(position);
            deleteTileAndMove(position, tile);
        }
        if (direction == "left")
        {
            yield return new WaitForSeconds(digRate);
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
}

