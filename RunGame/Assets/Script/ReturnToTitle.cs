using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTitle : MonoBehaviour
{
    // �^�C�g����ʂ̃V�[����
    public string titleSceneName = "Title";

    // �^�C�g����ʂɖ߂郁�\�b�h
    public void ReturnToTitleScreen()
    {
        // �w�肳�ꂽ�V�[�������[�h���ă^�C�g����ʂɖ߂�
        SceneManager.LoadScene(titleSceneName);
    }
}
