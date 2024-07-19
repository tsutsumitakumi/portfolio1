using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Panel : MonoBehaviour
{
    public GameDirecter _directer;
    // Start is called before the first frame update
    void Start()
    {
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
