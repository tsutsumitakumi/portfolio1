using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class minuteHandManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int m = DateTime.Now.Minute;
        GetComponent<Transform>().localEulerAngles = new Vector3(0, 0, -360 / 60.0f * m);
    }
}