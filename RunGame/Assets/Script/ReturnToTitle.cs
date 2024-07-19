using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTitle : MonoBehaviour
{
    // タイトル画面のシーン名
    public string titleSceneName = "Title";

    // タイトル画面に戻るメソッド
    public void ReturnToTitleScreen()
    {
        // 指定されたシーンをロードしてタイトル画面に戻る
        SceneManager.LoadScene(titleSceneName);
    }
}
