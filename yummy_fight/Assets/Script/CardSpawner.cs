using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CardSpawner : MonoBehaviourPunCallbacks
{
    public GameObject cardPrefab; // カードのプレハブ

    // カードを特定のフィールドエリアに生成するメソッド
    public void SpawnCardOnField(int cardID, Vector3 position, Quaternion rotation, bool isFaceUp, bool isOpponentsField)
    {
        // カードのデータを含むオプショナルなパラメータ
        object[] cardData = new object[] { cardID, isFaceUp };

        // カードをインスタンス化し、全クライアントで同期
        GameObject card = PhotonNetwork.Instantiate(cardPrefab.name, position, rotation, 0, cardData);

        // 相手のフィールドに配置する場合は位置と向きを調整
        if (isOpponentsField)
        {
            // 例: 相手のフィールドにカードを表示するための位置と向きの調整
            card.transform.Rotate(0f, 180f, 0f); // カードを180度回転させて相手に向ける
        }
    }
}
