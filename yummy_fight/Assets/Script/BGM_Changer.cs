using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Changer : MonoBehaviour
{
    GameDirecter _directer;
    public AudioSource common, life1;
    bool life;
    // Start is called before the first frame update
    void Start()
    {
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
    }

    // Update is called once per frame
    void Update()
    {
        if((_directer.enemy_life <= 1 || _directer.player_life <= 1 )&& !life)
        {
            common.Stop();
            life1.Play();
            life = true;
        }
    }
}
