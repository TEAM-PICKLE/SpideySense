using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public WaveData[] waves;
    public ObjectManager objectManager;
    [SerializeField]
    private GameObject[] spawners;
    public GameObject currentSpawner;
    private int waveNum = 0;

    private void Start()
    {
        //Invoke("UpdateSpawner", 1f);
    }
    void Update()
    {
        if(objectManager.enemies.Count < 1)
        {
            if (waveNum < waves.Length)
            {
                SpawnWave(waveNum);
            }
        }
    }

    public void SpawnWave(int i)
    {
        for (int j=0; j<8; j++) // for each spawner
        {
            currentSpawner = spawners[j];
            Spawner spawnScript = currentSpawner.GetComponent<Spawner>();
            spawnScript.spawnSpeed = waves[i].enemySpeed[j];
            if (waves[i].activeSpawners[j])
            {
                spawnScript.SpawnEnemy();
            }
        }
        waveNum++;
    }

    IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1f);
    }
}
