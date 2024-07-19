using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Power : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject card;
    void Start()
    {
        if(card.GetComponent<CardMovement>().cardParent.gameObject.gameObject.CompareTag("Enemy"))
        {
            this.gameObject.transform.Rotate(new Vector3(0, 180, 180));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
