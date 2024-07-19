using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public GameObject cardObject;
    public GameDirecter _directer;
    public CardController _controller;
    private Button attackButton;
    public bool hirou;
    private SE_Controller SE;

    void Start()
    {
        // ゲームディレクターの参照を取得
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        // SEコントローラーの参照を取得
        SE = GameObject.Find("SE").GetComponent<SE_Controller>();
        // カードコントローラーの参照を取得
        _controller = cardObject.GetComponent<CardController>();
        // ボタンコンポーネントを取得
        attackButton = GetComponent<Button>();
        // ボタンを非表示にする
        this.gameObject.SetActive(false);
    }

    public void OnAttackButtonClick()
    {
        Debug.Log("相手に攻撃した");
        // プレイヤーの攻撃フラグを設定
        _directer.playerattack = true;
        // カードの攻撃フラグを設定
        cardObject.GetComponent<CardController>().attack = true;
        // カードを横向きにするメソッドを呼び出す
        _controller.RotateCard();
        // プレイヤーに攻撃するメソッドを呼び出す
        AttackPlayer();
        // ボタンを非表示にする
        gameObject.SetActive(false);
    }

    void AttackPlayer()
    {
        // カードIDが104の場合、特別な効果を発動
        if (cardObject.GetComponent<CardView>().cardID == 104)
        {
            Debug.Log("チーバガ効果発動");
            cardObject.GetComponent<EX_Card_Ability>().StartCoroutine("Chibaga");
            _directer.Koukahatudou = true;
        }

        // 敵のフィールドにカードがない場合、攻撃をキャンセルし敵のライフを減らす
        if (_directer.EnemyFieldCardList.Length == 0)
        {
            cardObject.GetComponent<CardController>().attack = false;
            _directer.playerattack = false;
            _directer.enemy_life--;
        }
    }
}
