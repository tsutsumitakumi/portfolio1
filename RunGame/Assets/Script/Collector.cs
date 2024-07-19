using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    // �g���K�[�ɑ��̃I�u�W�F�N�g���������Ƃ��ɌĂяo����郁�\�b�h
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����Փ˂����I�u�W�F�N�g�� "Ground" �܂��� "Obstacle" �^�O�������Ă���ꍇ
        if (collision.CompareTag("Ground") || collision.CompareTag("Obstacle"))
        {
            // ���̃I�u�W�F�N�g���A�N�e�B�u�ɂ���
            collision.gameObject.SetActive(false);
        }
    }
}
