using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BGM�̃X���C�_�[
public class BgmManager : MonoBehaviour
{
    // ���ʒ���
    public Slider slider;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // �X���C�_�[�𓮂������Ƃ��ɉ��ʂ�ύX����
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }
}
