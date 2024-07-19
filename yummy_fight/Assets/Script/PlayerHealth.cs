using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5; // 最大体力
    public int currentHealth; // 現在の体力
    public GameObject blockButton; // ブロックボタン
    public GameObject lifeButton; // ライフボタン

    void Start()
    {
        currentHealth = maxHealth; // ゲーム開始時に現在の体力を最大体力に設定
    }

    public void TakeDamage(int damageAmount)
    {
        if (!blockButton.activeSelf) // ブロックボタンが非アクティブの場合
        {
            blockButton.SetActive(true); // ブロックボタンを表示する
        }

        if (!lifeButton.activeSelf) // ライフボタンが非アクティブの場合
        {
            lifeButton.SetActive(true); // ライフボタンを表示する
        }

        currentHealth -= damageAmount; // ダメージを受けた分だけ現在の体力を減らす

        if (currentHealth <= 0)
        {
            Die(); // 体力が0以下になったら死亡処理を実行
        }
    }

    void Die()
    {
        // プレイヤーが死亡した際の処理をここに記述
        // 例えば、ゲームオーバー画面を表示する、リスポーンするなどの処理を記述
        Debug.Log("Player died!");
    }
}
