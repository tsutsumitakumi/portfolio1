using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    // シングルトンパターンのインスタンス
    private static RestartButton instance;

    private void Awake()
    {
        // シングルトンパターンの実装
        /*if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
    }

    public void RestartGame()
    {
        // ゲームをリスタートする前に、DontDestroyオブジェクトを破棄
        //Destroy(DontDestroy.instance.gameObject);

        Debug.Log("りすたーと");

        // 現在のシーンを再読み込みする
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // ゲームの時間を通常に戻す
        Time.timeScale = 1f;
    }
}
