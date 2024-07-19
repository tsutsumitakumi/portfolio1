using UnityEngine;

public class CustomClock : MonoBehaviour
{
    // ���S�ƂȂ鎞�ԁi24���Ԍ`���j
    public int centerHour = 6;
    public int centerMinute = 0;

    // ���v�̐j�̑��x
    public float hourHandSpeed = 30.0f; // 30 degrees per hour
    public float minuteHandSpeed = 6.0f; // 6 degrees per minute

    // Update is called once per frame
    void Update()
    {

        // ���S���Ԃ���̍������v�Z
        int hourDifference = centerHour;
        int minuteDifference = centerMinute;

        // ���v�̐j�̊p�x���v�Z
        float hourHandAngle = -(hourDifference * hourHandSpeed);
        float minuteHandAngle = -(minuteDifference * minuteHandSpeed);


        if (centerMinute % 5 == 0)
        {

        }

        // ���v�̐j�̊p�x���X�V
        transform.Find("HourHand").localEulerAngles = new Vector3(0, 0, hourHandAngle);
        transform.Find("MinuteHand").localEulerAngles = new Vector3(0, 0, minuteHandAngle);

        // ���j�ɘA�����Ď��j������
        float minuteHandAdjustedAngle = -((minuteDifference / 5) * 2.0f); // Adjust based on minute hand position
        //float combinedMinuteHandAngle = minuteHandAngle + minuteHandAdjustedAngle;
        transform.Find("HourHand").localEulerAngles = new Vector3(0, 0, minuteHandAdjustedAngle);
    }
}



