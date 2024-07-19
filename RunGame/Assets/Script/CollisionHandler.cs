using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // 2D�R���C�_�[���g���K�[�ɓ������Ƃ��ɌĂяo����郁�\�b�h
    void OnTriggerEnter2D(Collider2D other)
    {
        // �����Փ˂����I�u�W�F�N�g�� "Obstacle" �^�O�������Ă���ꍇ
        if (other.CompareTag("Obstacle"))
        {
            GameOver(); // �Q�[���I�[�o�[�������Ăяo��
        }
    }

    // �Q�[���I�[�o�[����
    void GameOver()
    {
        // �Q�[���I�[�o�[��ʂɑJ��
        SceneManager.LoadScene("GameOver");
    }
}
