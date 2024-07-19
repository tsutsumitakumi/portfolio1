using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDrop : MonoBehaviour
{
    public GameObject target;

    bool check = false;
    void Start()
    {

    }
    void Update()
    {
        if (check)
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void SphereGravity()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void CheckEventHandler(Collider2D collider)
    {
        check = true;
    }

}
