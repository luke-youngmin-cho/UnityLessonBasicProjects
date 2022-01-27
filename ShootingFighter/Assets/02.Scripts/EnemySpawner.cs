using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool doSpawn;
    
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnDelay;
    float elapsedTime;
    private void Update()
    {
        if(elapsedTime > spawnDelay)
        {
            Instantiate(enemyPrefab, transform);
            elapsedTime = 0;
        }
        elapsedTime += Time.deltaTime;
    }
}
