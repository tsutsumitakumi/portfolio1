using System;
using UnityEngine;

public class hourHandsManager : MonoBehaviour
{
    // 自分で指定する中心となる時間（24時間形式）
    public int startHour = 12;
    public int startMinute = 0;

    // 1秒あたりの仮想時間の進行速度
    public float timeSpeed = 1.0f;

    private void Update()
    {
        // 現在の時間と指定した中心時間の差分を計算
        TimeSpan timeDifference = DateTime.Now.TimeOfDay - new TimeSpan(startHour, startMinute, 0);

        // 指定した中心時間からの経過時間に応じて時計の針を更新
        UpdateClockHands(timeDifference);
    }

    private void UpdateClockHands(TimeSpan elapsedTime)
    {
        // 時針の角度を計算
        float hourHandAngle = -((360 / 12.0f) * ((float)elapsedTime.TotalHours % 12));

        // 分針の角度を計算
        float minuteHandAngle = -((360 / 60.0f) * ((float)elapsedTime.TotalMinutes % 60));

        // 時計の針の角度を更新
        transform.Find("hourHand").localEulerAngles = new Vector3(0, 0, hourHandAngle);
        transform.Find("minuteHand").localEulerAngles = new Vector3(0, 0, minuteHandAngle);
    }
}


