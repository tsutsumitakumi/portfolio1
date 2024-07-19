using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    // �Q�[���I�[�o�[�ƂȂ�Y���W��臒l
    public float deathYPosition = -4f;

    void Update()
    {
        // �v���C���[��Y���W��臒l�ȉ����ǂ������`�F�b�N
        if (transform.position.y <= deathYPosition)
        {
            GameOver();
        }
    }

    // �Q�[���I�[�o�[����
    void GameOver()
    {
        // �Q�[���I�[�o�[��ʂɑJ��
        SceneManager.LoadScene("GameOver");
    }
}
