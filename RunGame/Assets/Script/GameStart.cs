using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public string startSceneName = "Start"; // タイトル画面のシーン名

    // タイトル画面に戻るメソッド
    public void ReturnToTitleScreen()
    {
        // 指定されたシーンをロード
        SceneManager.LoadScene(startSceneName);
    }
}
