using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerPos; // �v���C���[�̈ʒu��ێ����邽�߂̕ϐ�

    [SerializeField]
    private float offSetX = -6f; // �J������X�����̃I�t�Z�b�g

    private Vector3 tempPos; // �ꎞ�I�Ȉʒu��ێ����邽�߂̕ϐ�

    private void Awake()
    {
        FindPlayer(); // �v���C���[��������
    }

    private void LateUpdate()
    {
        FollowPlayer(); // �v���C���[��ǔ�����
    }

    // �v���C���[�̈ʒu���擾���郁�\�b�h
    private void FindPlayer()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
    }

    // �v���C���[��ǔ����郁�\�b�h
    private void FollowPlayer()
    {
        if (playerPos)
        {
            tempPos = transform.position; // ���݂̃J�����ʒu���擾
            tempPos.x = playerPos.position.x - offSetX; // �v���C���[�̈ʒu�ɃI�t�Z�b�g��K�p

            transform.position = tempPos; // �J�����ʒu���X�V
        }
    }
}
