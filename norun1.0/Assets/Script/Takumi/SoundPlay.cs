using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    // AudioSource�R���|�[�l���g
    private AudioSource audioSource;

    void Start()
    {
        // ���̃I�u�W�F�N�g��AudioSource�R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayStart()
    {
        // �N���b�v����x�����Đ�
        audioSource.PlayOneShot(audioSource.clip);
    }
}
