using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBGM : MonoBehaviour
{
    AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        Invoke("saisei", 0.6f);
    }

    void saisei()
    {
        Audio.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
