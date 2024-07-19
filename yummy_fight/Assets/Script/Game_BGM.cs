using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource BGM1;
    [SerializeField] AudioSource BGM2;
    GameDirecter _directer;
    void Start()
    {
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        BGM1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(_directer.player_life <= 1 || _directer.enemy_life <= 1)
        {
            BGM1.Stop();
            BGM2.Play();
        }
    }
}
