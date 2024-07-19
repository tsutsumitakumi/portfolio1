using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    // �v���C���[�̈ړ����x
    private float moveSpeed = 8f;

    private Rigidbody2D rb;          // Rigidbody2D�R���|�[�l���g
    private float lastPositionX;     // �O���X���W
    private float timeSinceLastMove; // �O��̈ړ�����̌o�ߎ���

    // �Q�[���I�[�o�[�ɂȂ�܂ł̒�~���Ԃ�臒l
    public float gameOverTimeThreshold = 1.5f;

    // Start���\�b�h�ŏ�����
    private void Start()
    {
        lastPositionX = transform.position.x; // �����ʒu��X���W��ۑ�
        timeSinceLastMove = 0f;               // �o�ߎ��Ԃ����Z�b�g
    }

    // Awake���\�b�h�ŃR���|�[�l���g���擾
    private void Awake()
    {
        // Rigidbody2D�R���|�[�l���g���擾
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate���\�b�h�ŕ����v�Z���s��
    private void FixedUpdate()
    {
        MovePlayer();       // �v���C���[���ړ�
        CheckForGameOver(); // �Q�[���I�[�o�[�̏������`�F�b�N
    }

    // �v���C���[���ړ����郁�\�b�h
    private void MovePlayer()
    {
        // �w�葬�x�ňړ�
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    // �Q�[���I�[�o�[�̏������`�F�b�N���郁�\�b�h
    private void CheckForGameOver()
    {
        // ���݂�X���W�ƑO���X���W���������ǂ������`�F�b�N
        if (transform.position.x == lastPositionX)
        {
            // �����ʒu�̏ꍇ�A�o�ߎ��Ԃ𑝂₷
            timeSinceLastMove += Time.fixedDeltaTime;
            // ��莞�Ԍo�߂����ꍇ�̓Q�[���I�[�o�[�V�[���ɑJ�ڂ���
            if (timeSinceLastMove >= gameOverTimeThreshold)
            {
                Debug.Log("Game Over: Player didn't move for too long.");
                // �Q�[���I�[�o�[�V�[���ɑJ�ڂ���
                SceneManager.LoadScene("GameOver");
            }
        }
        else
        {
            // X���W���ς�����ꍇ�A�o�ߎ��Ԃ����Z�b�g
            timeSinceLastMove = 0f;
            // ���݂�X���W���X�V
            lastPositionX = transform.position.x;
        }
    }
}
