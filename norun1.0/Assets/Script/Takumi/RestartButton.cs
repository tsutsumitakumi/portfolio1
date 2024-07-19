using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    // �V���O���g���p�^�[���̃C���X�^���X
    private static RestartButton instance;

    private void Awake()
    {
        // �V���O���g���p�^�[���̎���
        /*if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
    }

    public void RestartGame()
    {
        // �Q�[�������X�^�[�g����O�ɁADontDestroy�I�u�W�F�N�g��j��
        //Destroy(DontDestroy.instance.gameObject);

        Debug.Log("�肷���[��");

        // ���݂̃V�[�����ēǂݍ��݂���
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // �Q�[���̎��Ԃ�ʏ�ɖ߂�
        Time.timeScale = 1f;
    }
}
