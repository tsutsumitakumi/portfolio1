using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Synchronization : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject enemyField; // Enemy_field エリアをアサイン

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void SummonCard(int cardID)
    {
        // カードIDを相手に送信
        photonView.RPC("ReceiveCardID", RpcTarget.Others, cardID);
    }

    // ネットワーク越しに呼び出されるメソッド
    [PunRPC]
    private void ReceiveCardID(int cardID)
    {
        // カードのプレハブをインスタンス化
        GameObject cardObject = PhotonNetwork.Instantiate("CardPrefab", Vector3.zero, Quaternion.identity);

        // インスタンス化されたオブジェクトから Card クラスを取得せずに、直接表示処理を呼ぶ
        DisplayCardInformation(cardObject, cardID);
    }

    // カードを相手のフィールドに移動させるメソッド
    private void MoveCardToEnemyField(GameObject cardObject)
    {
        // カードが持つ Transform コンポーネントを取得
        Transform cardTransform = cardObject.transform;

        // 相手のフィールド上に移動させる
        cardTransform.SetParent(enemyField.transform);

        // 位置や回転などの調整が必要であれば、適切に設定する
        cardTransform.localPosition = Vector3.zero; // 例として中央に配置
        cardTransform.localRotation = Quaternion.identity; // 回転を初期化
        cardTransform.localScale = Vector3.one; // スケールを初期化
    }
    // カードの情報を表示するメソッド
    private void DisplayCardInformation(GameObject cardObject, int cardID)
    {
        // カードが持つ CardEntity を取得
        CardEntity cardEntity = GetCardEntity(cardID);

        if (cardEntity != null)
        {
            // カードオブジェクトが持つ SpriteRenderer コンポーネントを取得
            SpriteRenderer spriteRenderer = cardObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                // CardEntity からアイコンを取得して SpriteRenderer に設定
                spriteRenderer.sprite = cardEntity.icon;
            }
            else
            {
                Debug.LogError("SpriteRenderer not found on the card object.");
            }
        }
        else
        {
            Debug.LogError("CardEntity not found for card ID: " + cardID);
        }
    }

    // CardIDに対応するCardEntityを取得するメソッド
    private CardEntity GetCardEntity(int cardID)
    {
        // ここに実際のデータ取得処理を追加
        // このサンプルでは ScriptableObject を使用しているため、Resources フォルダから取得する例を示します。
        // 実際のプロジェクトでは、データベースや別のデータ管理クラスを利用することが一般的です。

        string cardEntityPath = "CardEntities/CardEntity_" + cardID; // リソースのパス
        CardEntity cardEntity = Resources.Load<CardEntity>(cardEntityPath);

        if (cardEntity != null)
        {
            return cardEntity;
        }
        else
        {
            Debug.LogError("CardEntity not found for card ID: " + cardID);
            return null;
        }
    }
}