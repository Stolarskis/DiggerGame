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
        int[] proceduralLayer = new int[8];
        proceduralLayer[0] = 5000;
        int depth = 0;
        int oreSelector = 4;
        proceduralLayer[1] = 250;
        proceduralLayer[2] = 250;
        proceduralLayer[3] = 75;



        int[] incrementWeightValues = new int[8];
        //Dirt
        incrementWeightValues[0] = 0;
        //Copper
        incrementWeightValues[1] = 0;
        //Iron
        incrementWeightValues[2] = 0;
        //Gold
        incrementWeightValues[3] = 5; 
        //Plat
        incrementWeightValues[4] = 15;
        //Emerald
        incrementWeightValues[5] = 10;
        //Ruby
        incrementWeightValues[6] = 5;
        //Diamond
        incrementWeightValues[7] = 1;


        for(int i = 0; i < 10; i++)
        {
            //Debug.Log(depth);
            proceduralLayer = incrementWeights(proceduralLayer, oreSelector, incrementWeightValues);
            renderMap(new int[100,50], level,0,depth,proceduralLayer,calcTotalWeight(proceduralLayer));
            if(oreSelector < proceduralLayer.Length)
            {
                oreSelector++;
            }
            depth = depth -50;
        }
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
        return result;
    }

    private void displayOreTally()
    {
        Debug.Log(oreTally[0]);
        Debug.Log(oreTally[1]);
        Debug.Log(oreTally[2]);
        Debug.Log(oreTally[3]);
    }

    public int[] incrementWeights(int[] weights, int upTo, int[] weightIncreaseArr)
    {
        if (weights == null){
            return null; 
        }
        for (int i = 0; i < upTo; i++)
        {
            weights[i] += weightIncreaseArr[i];
        }
        return weights;
    }
} 
