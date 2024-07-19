using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerPos; // プレイヤーの位置を保持するための変数

    [SerializeField]
    private float offSetX = -6f; // カメラのX方向のオフセット

    private Vector3 tempPos; // 一時的な位置を保持するための変数

    private void Awake()
    {
        FindPlayer(); // プレイヤーを見つける
    }

    private void LateUpdate()
    {
        FollowPlayer(); // プレイヤーを追尾する
    }

    // プレイヤーの位置を取得するメソッド
    private void FindPlayer()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
    }

    // プレイヤーを追尾するメソッド
    private void FollowPlayer()
    {
        if (playerPos)
        {
            tempPos = transform.position; // 現在のカメラ位置を取得
            tempPos.x = playerPos.position.x - offSetX; // プレイヤーの位置にオフセットを適用

            transform.position = tempPos; // カメラ位置を更新
        }
    }
}
