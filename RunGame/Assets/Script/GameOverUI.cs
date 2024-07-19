using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    // 経過時間を表示するテキスト
    public Text elapsedTimeText; 

    void Start()
    {
        DisplayElapsedTime();          // 経過時間を表示
        TimeManager.SaveElapsedTime(); // 経過時間をリセット
    }

    // 経過時間を表示するメソッド
    void DisplayElapsedTime()
    {
        // PlayerPrefsから保存された経過時間を取得
        float elapsedTime = PlayerPrefs.GetFloat("ElapsedTime", 0f);

        // 分と秒を計算
        string minutes = ((int)elapsedTime / 60).ToString();
        string seconds = (elapsedTime % 60).ToString("f2");

        // 経過時間をテキストに表示
        elapsedTimeText.text = "Time: " + minutes + ":" + seconds;
    }
}
