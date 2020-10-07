using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _asteroidPrefab;

    [SerializeField] private float _delayBetweenSpawn;

    [SerializeField] private float _spawnRange = 10;

    private float _timer = 1;

    private void Update()
    {
        if(_timer <= 0)
        {
            //spawn asteroid
            GameObject spawnedObject = Instantiate(_asteroidPrefab);
            spawnedObject.transform.position = Random.insideUnitCircle * _spawnRange;

            _timer = _delayBetweenSpawn;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}
