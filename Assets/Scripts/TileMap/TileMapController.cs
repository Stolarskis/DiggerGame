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

    private int[] tileWeights;
    private int weightTotal;
    private int[] oreTally;

    private Hashtable tileNums = new Hashtable();
    

    private void Awake()
    {
        tileWeights = new int[4]; //number of things
        oreTally = new int[4];
        //weighting of each thing, high number means more occurrance
        tileWeights[0] = 50;
        tileWeights[1] = 8;
        tileWeights[2] = 8;
        tileWeights[3] = 5;

        weightTotal = 0;
        foreach (int w in tileWeights)
        {
            weightTotal += w;
        }
        RandomWeighted();
        renderMap(generateArray(100, 100, true), level);
        displayOreTally();
    }

    public static int[,] generateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                if (empty)
                {
                    map[x, y] = 0;
                }
                else
                {
                    map[x, y] = 1;
                }
            }
        }
        return map;
    }

    public static int[,] generateTileMakeup(int[,] map, float seed)
    {
        int newPoint = 0;
        //Used to reduced the position of the Perlin point
        float reduction = 0.5f;
        //Create the Perlin
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            //Make sure the noise starts near the halfway point of the height
            newPoint += (map.GetUpperBound(1) / 2);
            for (int y = newPoint; y >= 0; y--)
            {
                map[x, y] = 1;
            }
        }
        return map;
    }
    public static int[,] perlinNoise(int[,] map, float seed)
    {
        int newPoint;
        //Used to reduced the position of the Perlin point
        float reduction = 0.5f;
        //Create the Perlin
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, seed) - reduction) * map.GetUpperBound(1));

            //Make sure the noise starts near the halfway point of the height
            newPoint += (map.GetUpperBound(1) / 2);
            for (int y = newPoint; y >= 0; y--)
            {
                map[x, y] = 1;
            }
        }
        return map;
    }

    public void renderMap(int[,] map, Tilemap tilemap)
    {
        //Clear the map (ensures we dont overlap)
        tilemap.ClearAllTiles();
        //Loop through the width of the map
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            //Loop through the height of the map
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                int tileNum = RandomWeighted();
                //Debug.Log(tileNum);
               tilemap.SetTile(new Vector3Int(x, y, 0), tiles[tileNum]);
            }
        }
    }

    private int RandomWeighted()
    {
        int result = 0, total = 0;
        int randVal = Random.Range(0,weightTotal);
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
