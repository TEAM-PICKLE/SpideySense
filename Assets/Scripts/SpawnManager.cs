using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public WaveData[] waves;
    bool isWaving;
    [SerializeField]
    private GameObject[] spawners = new GameObject[8];

    public void SpawnWave(int i)
    {
        if (!isWaving)
        {
            isWaving = true;
            for(int j=0; j<8; j++)
            {
                if (waves[i].activeSpawners[j])
                {
                    // set speed
                    spawners[j].SetActive(true);
                }
            }
            //if all obj are gone
            StartCoroutine(WaitOneSecond());
            isWaving = false;
        }
    }

    IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1f);
    }
}
