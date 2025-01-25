using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    public bool isSpawningEnemies;
    private float spawnCooldown;
    [SerializeField]
    private float minCooldown = 2f;
    [SerializeField]
    private float maxCooldown = 10f;

    void Update()
    {
        // test
        if (!isSpawningEnemies)
        {
            StartCoroutine(SpawnEnemy());
        }
        
    }

    IEnumerator SpawnEnemy()
    {
        // redo to spawn when isActive
        spawnCooldown = Random.Range(minCooldown, maxCooldown);
        isSpawningEnemies = true;
        yield return new WaitForSeconds(spawnCooldown);
        Instantiate(enemyPrefab, transform);
        isSpawningEnemies = false;
    }
}
