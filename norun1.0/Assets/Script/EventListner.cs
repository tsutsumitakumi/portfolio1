using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListner : MonoBehaviour
{

    public void CheckEventHandler(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")){
            Debug.Log("�߂Â���");

            var rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
   
    }

    public void HitEventHandler(Collider2D collider)
    {
        Debug.Log("��������");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
