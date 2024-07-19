using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // �o�ߎ��Ԃ�ێ�����ϐ�
    public static float elapsedTime = 0f;

    void Update()
    {
        // ���t���[���̌o�ߎ��Ԃ����Z
        elapsedTime += Time.deltaTime;
    }

    // �o�ߎ��Ԃ�ۑ����郁�\�b�h
    public static void SaveElapsedTime()
    {
        // �o�ߎ��Ԃ�PlayerPrefs�ɕۑ�
        PlayerPrefs.SetFloat("ElapsedTime", elapsedTime);
        // PlayerPrefs�̕ύX��ۑ�
        PlayerPrefs.Save();
    }
}
