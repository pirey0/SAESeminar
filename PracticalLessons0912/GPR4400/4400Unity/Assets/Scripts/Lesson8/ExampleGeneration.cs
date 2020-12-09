using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleGeneration : MonoBehaviour
{
    [SerializeField] TerrainData terrainData;

    private void Start()
    {
        float[,] heights = new float[terrainData.alphamapHeight, terrainData.alphamapWidth];
        for (int y = 0; y < heights.GetLength(0); y++)
        {
            for (int x = 0; x < heights.GetLength(1); x++)
            {
                heights[y, x] = (float)y / heights.GetLength(1);
            }
        }

        terrainData.SetHeights(0, 0, heights);

        TreeInstance[] trees = new TreeInstance[1000];


        for (int i = 0; i < trees.Length; i++)
        {
            var newTree = new TreeInstance();
            newTree.position = new Vector3(Random.Range(0, 1f), 0, Random.Range(0, 1f));
            newTree.prototypeIndex = 0;
            newTree.widthScale = 1f;
            newTree.heightScale = 1f;
            newTree.color = Color.white;
            newTree.lightmapColor = Color.white;

            trees[i] = newTree;
        }

        terrainData.SetTreeInstances(trees, true);
    }
}
