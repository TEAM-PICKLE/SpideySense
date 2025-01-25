using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSaber : MonoBehaviour
{
    public Rigidbody rb_handsaber;


    // Start is called before the first frame update
    void Start()
    { 
        rb_handsaber = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
