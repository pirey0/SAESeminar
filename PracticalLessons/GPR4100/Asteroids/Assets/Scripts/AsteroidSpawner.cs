using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _asteroidPrefab;


    [SerializeField] float _amountOnStart;

    [Range(0,6)]
    [SerializeField] float _safeRangeAroundOrigin;

    private float _timer = 1;

    private void Start()
    {
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = halfHeight * ((float)Screen.width / Screen.height);
      
        for (int i = 0; i < _amountOnStart; i++)
        {
            GameObject newAsteroid = Instantiate(_asteroidPrefab);
            while(newAsteroid.transform.position.magnitude < _safeRangeAroundOrigin)
            {
                newAsteroid.transform.position = new Vector3(Random.Range(-halfWidth, halfWidth), Random.Range(-halfHeight, halfHeight));
            }
        }
    }
}
