using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BGMのスライダー
public class BgmManager : MonoBehaviour
{
    // 音量調整
    public Slider slider;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // スライダーを動かしたときに音量を変更する
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }
}
