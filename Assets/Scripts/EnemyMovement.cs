using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float enemySpeed = 5f;
    [SerializeField]
    private GameObject movementTarget;
    private Vector3 thisToTarget;

    private float smoothTime = 0.25f;
    Vector3 currentVelocity;

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
