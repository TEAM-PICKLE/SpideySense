using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed = 5f;
    [SerializeField]
    private GameObject movementTarget;

    void Start()
    {
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
