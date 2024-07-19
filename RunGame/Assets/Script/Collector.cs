using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    // トリガーに他のオブジェクトが入ったときに呼び出されるメソッド
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // もし衝突したオブジェクトが "Ground" または "Obstacle" タグを持っている場合
        if (collision.CompareTag("Ground") || collision.CompareTag("Obstacle"))
        {
            // そのオブジェクトを非アクティブにする
            collision.gameObject.SetActive(false);
        }
    }
}
