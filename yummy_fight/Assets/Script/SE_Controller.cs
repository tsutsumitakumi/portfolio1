using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Controller : MonoBehaviour
{
    public AudioSource audio;

    public AudioClip ability, phase, life_break, draw,hakai;
    // Start is called before the first frame update
    void Start()
    {
        Ability_SE();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Ability_SE()
    {
        audio.PlayOneShot(ability);
    }
    public void phase_SE()
    {
        audio.PlayOneShot(phase);
    }

    public void life_break_SE()
    {
        audio.PlayOneShot(life_break);
    }

    public void draw_SE()
    {
        audio.PlayOneShot(draw);
    }

    public void hakai_SE()
    {
        audio.PlayOneShot(hakai);
    }
}
