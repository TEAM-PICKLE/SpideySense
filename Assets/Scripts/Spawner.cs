using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnSpeed; // set by SpawnManager, passed to EnemyMovement


    //public bool isSpawningEnemies;
    //private float spawnCooldown;
    //[SerializeField]
    //private float minCooldown = 2f;
    //[SerializeField]
    //private float maxCooldown = 10f;

    void Update()
    {
        // test
        //if (!isSpawningEnemies)
        //{
        //    StartCoroutine(SpawnEnemy());
        //}
        
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform);
    }

    //IEnumerator SpawnEnemy()
    //{
    //    spawnCooldown = Random.Range(minCooldown, maxCooldown);
    //    isSpawningEnemies = true;
    //    yield return new WaitForSeconds(spawnCooldown);
    //    Instantiate(enemyPrefab, transform);
    //    isSpawningEnemies = false;
    //}
}
