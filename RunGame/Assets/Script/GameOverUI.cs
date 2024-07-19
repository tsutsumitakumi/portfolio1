using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    // �o�ߎ��Ԃ�\������e�L�X�g
    public Text elapsedTimeText; 

    void Start()
    {
        DisplayElapsedTime();          // �o�ߎ��Ԃ�\��
        TimeManager.SaveElapsedTime(); // �o�ߎ��Ԃ����Z�b�g
    }

    // �o�ߎ��Ԃ�\�����郁�\�b�h
    void DisplayElapsedTime()
    {
        // PlayerPrefs����ۑ����ꂽ�o�ߎ��Ԃ��擾
        float elapsedTime = PlayerPrefs.GetFloat("ElapsedTime", 0f);

        // ���ƕb���v�Z
        string minutes = ((int)elapsedTime / 60).ToString();
        string seconds = (elapsedTime % 60).ToString("f2");

        // �o�ߎ��Ԃ��e�L�X�g�ɕ\��
        elapsedTimeText.text = "Time: " + minutes + ":" + seconds;
    }
}
