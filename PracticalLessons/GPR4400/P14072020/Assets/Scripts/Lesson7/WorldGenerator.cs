using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] int size = 10;
    [SerializeField] Vector2Int startLocation, endLocation;

    TileType[,] map;

    Transform player;
    Transform goal;

    void Start()
    {
        map = new TileType[size, size];


        for (int i = 0; i < size*Mathf.Log(size); i++)
        {
            int x = UnityEngine.Random.Range(0, size);
            int y = UnityEngine.Random.Range(0, size);

            map[x, y] = TileType.Wall;
        }

        for (int i = 0; i < size; i++)
        {
            map[0, i] = TileType.Wall;
            map[size - 1, i] = TileType.Wall;
            map[i, 0] = TileType.Wall;
            map[i, size - 1] = TileType.Wall;
        }


        SpawnVisuals();

    }

    private void SpawnVisuals()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (map[x, y] == TileType.Wall)
                {
                    var primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    primitive.transform.position = new Vector3(x, 0, y);
                }
            }
        }

        player = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
        goal = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;

        player.GetComponent<MeshRenderer>().material.color = Color.green;
        goal.GetComponent<MeshRenderer>().material.color = Color.yellow;

        player.position = new Vector3(startLocation.x, 0, startLocation.y);
        goal.position = new Vector3(endLocation.x, 0, endLocation.y);
    }

}

public enum TileType
{
    Floor,
    Wall
}
