using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    // AudioSourceコンポーネント
    private AudioSource audioSource;

    void Start()
    {
        // このオブジェクトのAudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayStart()
    {
        // クリップを一度だけ再生
        audioSource.PlayOneShot(audioSource.clip);
    }
}
