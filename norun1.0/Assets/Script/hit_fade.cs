using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_fade : MonoBehaviour
{
    public GameObject targetObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            targetObj.GetComponent<Fade>().Fadeout();

        }
    }
}
