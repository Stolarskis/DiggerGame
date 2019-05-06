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

    private Hashtable tileNums = new Hashtable();
    public bool generateLevelAtStart = false;
    

    private void Awake()
    {
        int[] initialWeights = new int[8] { 5000,50,50,20,0,0,0,0};
        initialWeights[0] = 5000;
        initialWeights[1] = 50;
        initialWeights[2] = 50;
        initialWeights[3] = 20;



        int[] weightScale = new int[8] {0, 10, 10, 10, 10, 10, 10, 10 };
        //For Algorithim Tuning Purposes Purposes
        //Dirt
        weightScale[0] = 0;
        //Copper
        weightScale[1] = 10;
        //Iron
        weightScale[2] = 10;
        //Gold
        weightScale[3] = 10; 
        //Plat
        weightScale[4] = 10;
        //Emerald
        weightScale[5] = 10;
        //Ruby
        weightScale[6] = 10; 
        //Diamond
        weightScale[7] = 10;

        //Generate the level

        if (generateLevelAtStart)
        {
            generateLevel(-1, initialWeights, weightScale, 10, new int[65, 100], 4);
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


    //GenerateLevel is responsible for creating the entire level as each scene is loaded. 

    //It starts out with a set of initial weights for each ore and a weightsScale to increment those weights with each layer. This is to simulate, 
    //rarer ores becoming more common with each layer. 
   public void generateLevel(int startingDepth, int[] initialWeights, int[] weightsScale, int numLayers, int[,] sizeLayer, int startingOre)
    {
        int sizeLayerLength = sizeLayer.GetLength(1)-1;
        int oreSelector = startingOre;
        int depth = startingDepth;
        int[] weights = initialWeights;
        for(int i = 0; i < numLayers; i++)
        {
            //Debug.Log(depth);
            weights = incrementWeights(weights, oreSelector,weightsScale);
            renderMap(sizeLayer, level,0,depth,weights,calcTotalWeight(weights));
            if(oreSelector < weights.Length)
            {
                oreSelector++;
            }
            depth = depth - sizeLayerLength;
        }
    }
} 
