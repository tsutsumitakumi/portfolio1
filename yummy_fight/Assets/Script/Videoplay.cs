using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class Videoplay : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GameObject.Find("Video").GetComponent<VideoPlayer>();
        videoPlayer.Play();
        Invoke("StopVideo", 7f);
    }

    void StopVideo()
    {
        videoPlayer.Stop();
    }
}
