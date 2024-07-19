using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    // プレイヤーの移動速度
    private float moveSpeed = 8f;

    private Rigidbody2D rb;          // Rigidbody2Dコンポーネント
    private float lastPositionX;     // 前回のX座標
    private float timeSinceLastMove; // 前回の移動からの経過時間

    // ゲームオーバーになるまでの停止時間の閾値
    public float gameOverTimeThreshold = 1.5f;

    // Startメソッドで初期化
    private void Start()
    {
        lastPositionX = transform.position.x; // 初期位置のX座標を保存
        timeSinceLastMove = 0f;               // 経過時間をリセット
    }

    // Awakeメソッドでコンポーネントを取得
    private void Awake()
    {
        // Rigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdateメソッドで物理計算を行う
    private void FixedUpdate()
    {
        MovePlayer();       // プレイヤーを移動
        CheckForGameOver(); // ゲームオーバーの条件をチェック
    }

    // プレイヤーを移動するメソッド
    private void MovePlayer()
    {
        // 指定速度で移動
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    // ゲームオーバーの条件をチェックするメソッド
    private void CheckForGameOver()
    {
        // 現在のX座標と前回のX座標が同じかどうかをチェック
        if (transform.position.x == lastPositionX)
        {
            // 同じ位置の場合、経過時間を増やす
            timeSinceLastMove += Time.fixedDeltaTime;
            // 一定時間経過した場合はゲームオーバーシーンに遷移する
            if (timeSinceLastMove >= gameOverTimeThreshold)
            {
                Debug.Log("Game Over: Player didn't move for too long.");
                // ゲームオーバーシーンに遷移する
                SceneManager.LoadScene("GameOver");
            }
        }
        else
        {
            // X座標が変わった場合、経過時間をリセット
            timeSinceLastMove = 0f;
            // 現在のX座標を更新
            lastPositionX = transform.position.x;
        }
    }
}
