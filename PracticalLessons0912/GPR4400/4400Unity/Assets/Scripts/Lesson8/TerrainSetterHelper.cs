using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSetterHelper : MonoBehaviour
{
    [SerializeField] TerrainData terrainData;

    /*
    //Object that contains the actual data of the terrain;
    [SerializeField] TerrainData terrainData;

    //Use this to set the heights after the generation
    terrainData.SetHeights();

    //To use perlin noise
    (x and y need to be smaller then 1)
    float noise = Mathf.PerlinNoise(x, y);  

    //height of the terrain
    int height =  terrainData.alphamapHeight;
    int width = terrainData.alphamatWidth;

    //How to set the trees
    var trees = new TreeInstance[1];
    for (int i = 0; i < trees.Length; i++)
    {
        TreeInstance newTree = new TreeInstance();
        int x = Random.Range(0, terrainData.alphamapWidth);
        int y = Random.Range(0, terrainData.alphamapHeight);

        position needs to be between 0 and 1 (local space)
        newTree.position = new Vector3((float)x / terrainData.alphamapWidth, 0, (float)y / terrainData.alphamapHeight);
    }

    terrainData.SetTreeInstances(trees, true);
    */
}
