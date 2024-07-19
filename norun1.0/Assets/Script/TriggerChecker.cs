using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerChecker : MonoBehaviour
{
    public UnityEvent<Collider2D> onColliderEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onColliderEnter.Invoke(collision);
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
