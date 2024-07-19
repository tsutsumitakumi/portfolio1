using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    public Animator animator; // �A�j���[�^�[�R���|�[�l���g�ւ̎Q��
    private bool isButtonClicked = false; // �{�^���N���b�N�̒ǐ՗p�ϐ�

    // �{�^���������ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    public void OnButtonClick()
    {
        isButtonClicked = true; // �{�^�����N���b�N���ꂽ�̂ŁA�t���O��true�ɐݒ�
    }

    void Update()
    {
        if (phase()) // phase�֐���true��Ԃ��ꍇ
        {
            animator.SetBool("IsFphase", true); // �A�j���[�V�����Đ�
            isButtonClicked = false; // �t���O�����Z�b�g
        }
    }

    // phase�֐�
    private bool phase()
    {
        // �{�^�����N���b�N���ꂽ���ǂ������`�F�b�N
        return isButtonClicked;
    }
}