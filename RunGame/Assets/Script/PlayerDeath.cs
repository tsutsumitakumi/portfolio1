using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    // ゲームオーバーとなるY座標の閾値
    public float deathYPosition = -4f;

    void Update()
    {
        // プレイヤーのY座標が閾値以下かどうかをチェック
        if (transform.position.y <= deathYPosition)
        {
            GameOver();
        }
    }

    // ゲームオーバー処理
    void GameOver()
    {
        // ゲームオーバー画面に遷移
        SceneManager.LoadScene("GameOver");
    }
}
