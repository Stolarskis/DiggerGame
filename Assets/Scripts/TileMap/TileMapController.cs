using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{
    public Tilemap level;
    public int width;
    public int height;
    public TileBase[] tiles;

    private int[] oreTally;

    private Hashtable tileNums = new Hashtable();
    

    private void Awake()
    {
        int[] dirtLayer = new int[4]; //number of things
        int[] goldLayer = new int[4];
        oreTally = new int[4];
        //weighting of each thing, high number means more occurrance
        dirtLayer[0] = 100;
        dirtLayer[1] = 5;
        dirtLayer[2] = 5;
        dirtLayer[3] = 1;

        goldLayer[3] = 10000;
        
        renderMap(new int[100,100], level,0,0,dirtLayer,calcTotalWeight(dirtLayer));
        renderMap(new int[100,100], level,100,-99,goldLayer,calcTotalWeight(goldLayer));
        //renderMap(new int[300,100], level,0,-198);

        //renderMap(generateArray(50, 50, true), level,-200,-200);
        //renderMap(generateArray(50, 50, true), level,-400,-400);
        displayOreTally();
    }

    public int calcTotalWeight(int[] tileWeights)
    {
        int total = 0;
        foreach (int weight in tileWeights)
        {
            total += weight;
        }
        return total;
    }

    public void renderMap(int[,] map, Tilemap tilemap, int startPositionX,int startPositionY, int[] tileWeights,int tileWeightTotal)
    {
        //Loop through the width of the map
        for (int y = 0; y < map.GetUpperBound(1); y++)
        {
            //Loop through the height of the map
            for (int x = 0; x < map.GetUpperBound(0); x++)
            {
                int tileNum = RandomWeight(tileWeights,tileWeightTotal);
               tilemap.SetTile(new Vector3Int(startPositionX+x, startPositionY, 0), tiles[tileNum]);
            }
            startPositionY -= 1;
        }
    }

    private int RandomWeight(int[] tileWeights, int tileWeightTotal)
    {
        int result = 0, total = 0;
        int randVal = Random.Range(0,tileWeightTotal+1);
        for (result = 0; result < tileWeights.Length; result++)
        {
            total += tileWeights[result];
            if (total >= randVal) break;
        }
        oreTally[result] += 1;
        return result;
    }

    private void displayOreTally()
    {
        Debug.Log(oreTally[0]);
        Debug.Log(oreTally[1]);
        Debug.Log(oreTally[2]);
        Debug.Log(oreTally[3]);
    }
} 
