using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // 2Dコライダーがトリガーに入ったときに呼び出されるメソッド
    void OnTriggerEnter2D(Collider2D other)
    {
        // もし衝突したオブジェクトが "Obstacle" タグを持っている場合
        if (other.CompareTag("Obstacle"))
        {
            GameOver(); // ゲームオーバー処理を呼び出す
        }
    }

    // ゲームオーバー処理
    void GameOver()
    {
        // ゲームオーバー画面に遷移
        SceneManager.LoadScene("GameOver");
    }
}
