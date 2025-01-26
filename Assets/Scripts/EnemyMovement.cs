using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed;
    public Spawner parentSpawnScript;
    [SerializeField]
    private GameObject movementTarget;
    public GameObject spawnManager;
    public ObjectManager objManager;

    private void Awake()
    {
        spawnManager = GameObject.Find("SpawnRig");
        objManager = spawnManager.GetComponent<ObjectManager>();
        objManager.AddEnemy(transform);
        Debug.Log(objManager.enemies.Count);
    }

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
        this.GetComponent<DamageComponent>().DestroySelf();
    }
}
