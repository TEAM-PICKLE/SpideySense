using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    new public int damageValue;
    new Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter (Collision collision)
    {

        if (collision.gameObject.GetComponent<PlayerHealth>()!=null)
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageValue);
            Invoke ("DestroySelf",0.25f);
        }

}

    public void DestroySelf()
    {
        //Play Particles here

        Destroy(this.gameObject);

    }
}
