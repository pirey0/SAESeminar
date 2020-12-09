
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainSetter : MonoBehaviour
{
    [SerializeField] TerrainData terrainData;
    [SerializeField] Vector2 noiseOffset;
    [SerializeField] Vector2 noiseScaling;
    [SerializeField] TreeInstance treeInstance;
    [SerializeField] int treeSeed;
    [SerializeField] int treeCount;
    [SerializeField] float treeMaxHeight = 0.5f;

    [SerializeField] bool update = false;

    private void Start()
    {
        UpdateTerrainData();
    }

    private void Update()
    {
        if (update)
        {
            update = false;
            UpdateTerrainData();
        }
    }

    private void UpdateTerrainData()
    {
        UpdateTerrainHeight();

        UpdateTrees();
    }

    private void UpdateTerrainHeight()
    {
        float[,] heights = new float[terrainData.alphamapHeight, terrainData.alphamapWidth];
        float[,,] texture = new float[terrainData.alphamapHeight, terrainData.alphamapWidth,2];

        for (int y = 0; y < heights.GetLength(0); y++)
        {
            for (int x = 0; x < heights.GetLength(1); x++)
            {
                float noiseX = noiseOffset.x + ((float)x / heights.GetLength(0)) * noiseScaling.x;
                float noiseY = noiseOffset.y + ((float)y / heights.GetLength(1)) * noiseScaling.y;

                float noise = Mathf.PerlinNoise(noiseX, noiseY);
                heights[y, x] = noise;
                texture[y, x, 0] = noise >0.5f ? 1:0;
                texture[y, x, 1] = noise > 0.5f ? 0 : 1;
            }
        }

        terrainData.SetAlphamaps(0, 0, texture);
        terrainData.SetHeights(0, 0, heights);
    }

    private void UpdateTrees()
    {
        Random.InitState(treeSeed);
        var trees = new TreeInstance[treeCount];

        for (int i = 0; i < trees.Length; i++)
        {
            int x = Random.Range(0, terrainData.alphamapWidth);
            int y = Random.Range(0, terrainData.alphamapHeight);

            //dont spawn trees to high in the mountains
            if(terrainData.GetHeight(x,y) > treeMaxHeight)
            {
                continue;
            }

            var newTree = new TreeInstance();
            newTree.position = new Vector3((float) x/ terrainData.alphamapWidth, 0, (float)y/terrainData.alphamapHeight);
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
