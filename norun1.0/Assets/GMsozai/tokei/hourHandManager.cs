using System;
using UnityEngine;

public class hourHandsManager : MonoBehaviour
{
    // �����Ŏw�肷�钆�S�ƂȂ鎞�ԁi24���Ԍ`���j
    public int startHour = 12;
    public int startMinute = 0;

    // 1�b������̉��z���Ԃ̐i�s���x
    public float timeSpeed = 1.0f;

    private void Update()
    {
        // ���݂̎��ԂƎw�肵�����S���Ԃ̍������v�Z
        TimeSpan timeDifference = DateTime.Now.TimeOfDay - new TimeSpan(startHour, startMinute, 0);

        // �w�肵�����S���Ԃ���̌o�ߎ��Ԃɉ����Ď��v�̐j���X�V
        UpdateClockHands(timeDifference);
    }

    private void UpdateClockHands(TimeSpan elapsedTime)
    {
        // ���j�̊p�x���v�Z
        float hourHandAngle = -((360 / 12.0f) * ((float)elapsedTime.TotalHours % 12));

        // ���j�̊p�x���v�Z
        float minuteHandAngle = -((360 / 60.0f) * ((float)elapsedTime.TotalMinutes % 60));

        // ���v�̐j�̊p�x���X�V
        transform.Find("hourHand").localEulerAngles = new Vector3(0, 0, hourHandAngle);
        transform.Find("minuteHand").localEulerAngles = new Vector3(0, 0, minuteHandAngle);
    }
}


