using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public string startSceneName = "Start"; // �^�C�g����ʂ̃V�[����

    // �^�C�g����ʂɖ߂郁�\�b�h
    public void ReturnToTitleScreen()
    {
        // �w�肳�ꂽ�V�[�������[�h
        SceneManager.LoadScene(startSceneName);
    }
}
