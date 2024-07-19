using UnityEngine;
using UnityEngine.UI;

public class ElapsedTimeDisplay : MonoBehaviour
{
    public Text elapsedTimeText; // 経過時間を表示するテキストUI
    private float startTime; // スタート時間

    void Start()
    {
        // スタート時間を現在の時間に設定
        startTime = Time.time;
    }

    void Update()
    {
        // 経過時間を計算
        float elapsedTime = Time.time - startTime;

        // 分と秒を計算
        string minutes = ((int)elapsedTime / 60).ToString();
        string seconds = (elapsedTime % 60).ToString("f2");

        // 経過時間をテキストUIに表示
        elapsedTimeText.text = "Time: " + minutes + ":" + seconds;
    }
}
