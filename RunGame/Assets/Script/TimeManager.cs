using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // 経過時間を保持する変数
    public static float elapsedTime = 0f;

    void Update()
    {
        // 毎フレームの経過時間を加算
        elapsedTime += Time.deltaTime;
    }

    // 経過時間を保存するメソッド
    public static void SaveElapsedTime()
    {
        // 経過時間をPlayerPrefsに保存
        PlayerPrefs.SetFloat("ElapsedTime", elapsedTime);
        // PlayerPrefsの変更を保存
        PlayerPrefs.Save();
    }
}
