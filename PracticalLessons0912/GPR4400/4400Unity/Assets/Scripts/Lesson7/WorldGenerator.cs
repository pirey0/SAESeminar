using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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


        for (int i = 0; i < size * Mathf.Log(size); i++)
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

        var path = AStar(startLocation, endLocation, AirDistance);
        
        if(path != null)
        {
            foreach (var p in path)
            {
                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.localScale = Vector3.one * 0.5f;
                go.GetComponent<MeshRenderer>().material.color = Color.yellow;
                go.transform.position = new Vector3(p.x, 0, p.y);
            }
        }
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

    private Vector2Int[] ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        Queue<Vector2Int> totalPath = new Queue<Vector2Int>();
        totalPath.Enqueue(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Enqueue(current);
        }

        return totalPath.ToArray();
    }


    private float AirDistance(Vector2Int from, Vector2Int to)
    {
        return Vector2Int.Distance(from, to);
    }

    delegate float HeuristicDelegate(Vector2Int v1, Vector2Int v2);

    private Vector2Int[] AStar(Vector2Int start, Vector2Int goal, HeuristicDelegate heuristic)
    {
        List<Vector2Int> openSet = new List<Vector2Int>();
        openSet.Add(start);

        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        Dictionary<Vector2Int, float> gScore = new Dictionary<Vector2Int, float>();
        InitiateToInfinity(ref gScore);
        gScore[start] = 0;

        Dictionary<Vector2Int, float> fScore = new Dictionary<Vector2Int, float>();
        InitiateToInfinity(ref fScore);
        fScore[start] = heuristic(start, goal);

        while (openSet.Count > 0)
        {
            openSet.Sort((a, b) => (int)fScore[a] - (int)fScore[b]);
            Vector2Int current = openSet[0];

            if (current == goal)
                return ReconstructPath(cameFrom, current);

            openSet.Remove(current);

            foreach (var neighbour in GetNeighboursOf(current))
            {
                float tentativeGScore = gScore[current] + GetEdgeWeightBetween(current, neighbour);

                if (tentativeGScore < gScore[neighbour])
                {
                    if (cameFrom.ContainsKey(neighbour))
                        cameFrom[neighbour] = current;
                    else
                        cameFrom.Add(neighbour, current);

                    gScore[neighbour] = tentativeGScore;
                    fScore[neighbour] = tentativeGScore + heuristic(neighbour, goal);
                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }

    float GetEdgeWeightBetween(Vector2Int current, Vector2Int neighbour)
    {
        if (map[current.x, current.y] == TileType.Wall)
            return float.MaxValue;
        else if (map[neighbour.x, neighbour.y] == TileType.Wall)
            return float.MaxValue;

        return 1;
    }

    Vector2Int[] GetNeighboursOf(Vector2Int node)
    {
        return new Vector2Int[] { new Vector2Int(node.x +1, node.y), new Vector2Int(node.x -1 , node.y),
            new Vector2Int(node.x, node.y+1), new Vector2Int(node.x, node.y-1) };
    }

    void InitiateToInfinity(ref Dictionary<Vector2Int, float> dict)
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                dict.Add(new Vector2Int(x, y), float.MaxValue);
            }
        }
    }

}

public enum TileType
{
    Floor,
    Wall
}

