using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed;
    public Spawner parentSpawnScript;
    [SerializeField]
    private GameObject movementTarget;

    void Start()
    {
        Spawner parentSpawnScript = GetComponentInParent<Spawner>();
        enemySpeed = parentSpawnScript.spawnSpeed;
        StartCoroutine(DestroyObject());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
