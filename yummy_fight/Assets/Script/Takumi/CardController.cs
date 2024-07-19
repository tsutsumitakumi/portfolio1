using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardController : MonoBehaviour
{
    // カードの見た目を管理するコンポーネント
    public CardView view;
    // カードのデータを管理するモデル
    public CardModel model;
    // カードの状態フラグ
    public bool hirou, attack, block, egumahu_aru;

    // 各種ゲーム管理の参照
    private GameDirecter _directer;
    private GameManager _manager;
    private SE_Controller SE;

    // UIボタンの参照
    public GameObject attack_button, blockbutton, kouka_button;
    // カードのパワーテキスト
    public GameObject power_text;
    // デフォルトのパワー値
    public int default_power;

    private void Awake()
    {
        // 各コンポーネントの参照を取得
        view = GetComponent<CardView>();
        SE = GameObject.Find("SE").GetComponent<SE_Controller>();
        default_power = view.power;
        hirou = false;
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // 各ボタンの参照を取得
        attack_button = transform.Find("Attack").gameObject;
        blockbutton = transform.Find("Block").gameObject;
        kouka_button = transform.Find("Kouka").gameObject;

        // ブロックボタンと効果ボタンを非表示にする
        blockbutton.SetActive(false);
        kouka_button.SetActive(false);
    }

    void Update()
    {
        // 敵の攻撃処理を呼び出す
        if (_directer.enemyattack)
        {
            enemyattack();
        }

        // カードのパワーテキストを更新
        power_text.GetComponent<TextMeshProUGUI>().text = view.power.ToString();

        // バトルフェーズの処理
        if (_directer.phase == GameDirecter.Phase.BATTLE || _directer.phase == GameDirecter.Phase.Enemy_BATTLE)
        {
            int cnt = 0;
            for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
            {
                if (_directer.playerFieldCardList[i].hirou && !_manager.egumahu)
                {
                    egumahu_aru = true;
                    cnt++;
                }
            }

            if (cnt == 0)
            {
                egumahu_aru = false;
            }
        }

        // ブロックボタンの表示/非表示の制御
        if (hirou)
        {
            blockbutton.SetActive(false);
        }
        else if (!hirou && _directer.phase == GameDirecter.Phase.Enemy_BATTLE
                 && GetComponent<CardMovement>().cardParent == GameObject.Find("Player_field").transform)
        {
            blockbutton.SetActive(true);
        }

        // 効果ボタンの表示/非表示の制御
        if ((_directer.Attackable || _directer.Blockable)
            && view.cardID == 102
            && !hirou
            && egumahu_aru)
        {
            kouka_button.SetActive(true);
        }
        else
        {
            kouka_button.SetActive(false);
        }
    }

    public void Init(int cardID)　// カードを生成した時に呼ばれる関数
    {
        Debug.Log(cardID);
        view.cardID = cardID;
        model = new CardModel(cardID); // カードデータを生成
        view.Show(model); // カードを表示
    }

    // カードを破壊
    public void Destroy_me()
    {
        Destroy(gameObject);
    }

    // カードを90度回転させます。
    public void RotateCard()
    {
        transform.Rotate(new Vector3(0f, 0f, 90f));
        transform.localScale = new Vector3(3.5f, 0.8f, 1.3f);
        hirou = true;
    }

    // カードのパワーをデフォルトに戻す
    public void power_back()
    {
        view.power = default_power;
    }

    // 敵の攻撃処理
    public void enemyattack()
    {
        transform.Rotate(new Vector3(0f, 0f, -90f));
        transform.localScale = new Vector3(3.5f, 0.8f, 1.3f);
        hirou = true;
        attack = true;
        _directer.Zekkouhyoujun = true;
        Player_Block();
        _directer.life_de_ukeru.SetActive(true);
    }

    // 敵のブロック処理
    public void enemyblock()
    {
        if (!hirou)
        {
            Debug.Log("CPU：ブロックします");
            transform.Rotate(new Vector3(0f, 0f, -90f));
            transform.localScale = new Vector3(3.5f, 0.8f, 1.3f);
            block = true;
            hirou = true;

            for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
            {
                if (_directer.playerFieldCardList[i].attack)
                {
                    _directer.Battle(_directer.playerFieldCardList[i].gameObject, gameObject);
                }
            }
        }
    }

    // プレイヤーのブロック処理
    public void Player_Block()
    {
        for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
        {
            if (!_directer.playerFieldCardList[i].hirou)
            {
                _directer.playerFieldCardList[i].GetComponent<CardController>().blockbutton.SetActive(true);
            }
        }
    }

    // カードを回復（カードを縦向きにする）
    public void kaihuku()
    {
        if (hirou)
        {
            transform.Rotate(new Vector3(0f, 0f, -90f));
            transform.localScale = new Vector3(1.3f, 2f, 1.3f);
            hirou = false;
        }
    }

    // カードを疲労状態にする（カードを横向きにする）
    public void Hirou()
    {
        if (!hirou)
        {
            transform.Rotate(new Vector3(0f, 0f, 90f));
            transform.localScale = new Vector3(3.5f, 0.8f, 1.3f);
            hirou = true;
        }
    }

    // 敵のカードを回復（カードを縦向きにする）
    public void kaihuku_Enemy()
    {
        if (hirou)
        {
            transform.Rotate(new Vector3(0f, 0f, 90f));
            transform.localScale = new Vector3(1.3f, 2f, 1.3f);
            hirou = false;
        }
    }
}
