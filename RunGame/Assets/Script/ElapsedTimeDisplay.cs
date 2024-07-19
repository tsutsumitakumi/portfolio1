using UnityEngine;
using UnityEngine.UI;

public class ElapsedTimeDisplay : MonoBehaviour
{
    public Text elapsedTimeText; // �o�ߎ��Ԃ�\������e�L�X�gUI
    private float startTime; // �X�^�[�g����

    void Start()
    {
        // �X�^�[�g���Ԃ����݂̎��Ԃɐݒ�
        startTime = Time.time;
    }

    void Update()
    {
        // �o�ߎ��Ԃ��v�Z
        float elapsedTime = Time.time - startTime;

        // ���ƕb���v�Z
        string minutes = ((int)elapsedTime / 60).ToString();
        string seconds = (elapsedTime % 60).ToString("f2");

        // �o�ߎ��Ԃ��e�L�X�gUI�ɕ\��
        elapsedTimeText.text = "Time: " + minutes + ":" + seconds;
    }
}
