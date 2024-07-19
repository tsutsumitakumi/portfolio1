using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirecter : MonoBehaviour
{

    public Player1[] playerList;//プレイヤーのリスト
    public bool Movable;//動けるか(スタンバイフェーズ)
    public bool Summonable;//召喚できるか(メインフェーズ)
    public bool Attackable;//攻撃できるか（バトルフェーズ）
    public bool Blockable;//防御できるか（バトルフェーズ）
    public bool Zekkouhyoujun;//箸休めが発動できるか
    public GameObject phase_text;//どのフェーズかを表示する
    public GameObject fade_panel;
    public GameObject Ensyutu;

    public GameManager manage_script;
    public CPU cpu_script;
    public GameObject before_outline;
    public GameObject before_outline_object;

    public CardController[] playerHandCardList;//プレイヤーの手札を格納するリスト
    public CardController[] playerFieldCardList;//フィールドのカードを格納するリスト
    public CardController[] playerkitchenCardList;//調理場のカードを格納するリスト
    public CardController[] enemyHandCardList;//敵の手札を格納するリスト
    public CardController[] EnemyKitchenCardList;//敵の調理場のカードを格納するリスト
    public CardController[] EnemyFieldCardList;//敵のフィールドのカードを格納するリスト

    public ObjectHighlight[] SearchImageList;//サーチするカード

    public GameObject p_text, e_text,life_de_ukeru;
    public int turn;
    public bool draw,main, battle;
    public int player_life, enemy_life;//プレイヤーとエネミーのライフ
    public bool enemyattack,playerattack,Koukahatudou,senityuu,enemyblock;

    public TextMeshProUGUI phaseText;// UIテキストをアサインするためのパブリック変数
    public Animator phaseAnimator;
    SE_Controller SE;
    public Button _button;
    public CPU _cpu;
    public bool E_turn;
    public bool E_draw;

    public enum Phase//フェーズ管理用列挙型変数

    {
        INIT,
        DRAW,
        STANDBY,
        MAIN,
        BATTLE,
        END,
        Enemy_INIT,
        Enemy_DRAW,
        Enemy_STANDBY,
        Enemy_MAIN,
        Enemy_BATTLE,
        Enemy_END,
    };

    public Phase phase;
    Player1 currentPlayer;
    void Start()
    {
        Ensyutu.SetActive(false);
        SE = GameObject.Find("SE").GetComponent<SE_Controller>();
        phase = Phase.INIT;
        fade_panel = GameObject.Find("Fade");
        fade_panel.SetActive(false);
        life_de_ukeru.SetActive(false);
        Movable = false;
        manage_script = GameObject.Find("GameManager").GetComponent<GameManager>();
        cpu_script = this.gameObject.GetComponent<CPU>();
        main = true;
        battle = true;
        player_life = 5;
        enemy_life = 5;

        _button = GetComponent<Button>();
        _cpu = GameObject.Find("GameDirecter").GetComponent<CPU>();
    }
    private Phase previousPhase;
    void Update()
    {
        p_text.GetComponent<TextMeshProUGUI>().text = player_life.ToString();
        e_text.GetComponent<TextMeshProUGUI>().text = enemy_life.ToString();

        //カードのリスト格納
        playerHandCardList = manage_script.playerHand.GetComponentsInChildren<CardController>();
        playerFieldCardList = manage_script.playerField.GetComponentsInChildren<CardController>();
        playerkitchenCardList = manage_script.playerKitchen.GetComponentsInChildren<CardController>();

        enemyHandCardList = manage_script.enemyHand.GetComponentsInChildren<CardController>();
        EnemyFieldCardList = manage_script.enemyField.GetComponentsInChildren<CardController>();
        EnemyKitchenCardList = manage_script.enemyKitchen.GetComponentsInChildren<CardController>();

        SearchImageList = manage_script.searchArea.GetComponentsInChildren<ObjectHighlight>();

        if(enemy_life <= 0 && !senityuu)
        {
            senityuu = true;
            StartCoroutine("Color_FadeOut");
        }


        switch (phase)
        {
            case Phase.INIT://初期フェーズ
                currentPlayer = playerList[0];
                InitPhase();
                break;
            case Phase.DRAW://ドローフェーズ
                Debug.Log("どろーふぇーずちぇっく");
                if (!draw)
                {
                    draw = true;
                    UpdatePhaseText();
                    Debug.Log("どろーふぇーず");
                    Debug.Log(draw);

                    Invoke("DrawPhase", 3.0f);
                }
                
                E_turn = false;
                break;
            case Phase.STANDBY://スタンバイ（移動）フェーズ
                draw = false;
                StandbyPhase();
                break;
            case Phase.MAIN://スタンバイ（移動）フェーズ
                MainPhase();
                break;
            case Phase.BATTLE://バトルフェーズ
                BattlePhase();
                break;
            case Phase.END://エンドフェーズ
                EndPhase();
                break;
            case Phase.Enemy_DRAW://ドローフェーズ
                turn++;
                Enemy_DrawPhase();
                E_turn = true;
                break;
            case Phase.Enemy_STANDBY://スタンバイ（移動）フェーズ
                Enemy_StandbyPhase();
                break;
            case Phase.Enemy_MAIN://メインフェーズ
                Enemy_MainPhase();
                break;
            case Phase.Enemy_BATTLE://バトルフェーズ
                Enemy_BattlePhase();
                break;
            case Phase.Enemy_END://エンドフェーズ
                Enemy_EndPhase();
                cpu_script.bans = false;
                cpu_script.mafin = false;
                break;
        }

        if (phase != previousPhase)
        {
            // フェーズインデックスを1に設定してアニメーションをトリガー
            phaseAnimator.SetInteger("phaseIndex", 1);

            // アニメーション再生後、フェーズインデックスを元に戻すための処理をスケジュール
            // 必要に応じて遅延を設定
            StartCoroutine(ResetPhaseIndexAfterDelay(0.5f));

            // 現在のフェーズを記録
            previousPhase = phase;
        }
        IEnumerator ResetPhaseIndexAfterDelay(float delay)
        {
            // 指定された遅延後に実行
            yield return new WaitForSeconds(delay);

            // フェーズインデックスを元に戻す（例: 0にリセット）
            phaseAnimator.SetInteger("phaseIndex", 0);
        }
    }
    void InitPhase()
    {
        Debug.Log("InitPhase");
        // フェーズ変更に伴うテキストの更新
        UpdatePhaseText();
        phase_text.GetComponent<TextMeshProUGUI>().text = currentPlayer+"\nInit";
        phase = Phase.DRAW;
        draw = false;
    }
    void DrawPhase()
    {
        Debug.Log("どろーーーーーーーーー");
        // フェーズ変更に伴うテキストの更新
        phase_text.GetComponent<TextMeshProUGUI>().text = currentPlayer + "\nDraw";
        currentPlayer.Draw();
        for(int i =0;i<playerFieldCardList.Length;i++)
        {
            playerFieldCardList[i].kaihuku();
        }
        phase = Phase.STANDBY;
        manage_script.Buns = false;
        manage_script.Patty = false;
        manage_script.Muffin = false;
        manage_script.Pickles = false;
        manage_script.Foodraw = false;
        manage_script.Plan = false;
        manage_script.Stop = false;
        manage_script.bagamute = false;
        manage_script.chibaga = false;
        manage_script.torabaga = false;
        manage_script.egumahu = false;
    }
    void StandbyPhase()
    {
        Debug.Log("StandbyPhase");
        // フェーズ変更に伴うテキストの更新
        UpdatePhaseText();
        phase_text.GetComponent<TextMeshProUGUI>().text = currentPlayer + "\nStandby";
        Movable = true;

        // StandbyPhaseに入ったときにカードの向きをリセット
        ResetCardRotation(playerFieldCardList);
    }

    // Player_fieldにあるカードの向きをリセットする関数
    public void ResetCardRotation(CardController[] cards)
    {
        
        foreach (var card in cards)
        {
            if (card != null && phase == Phase.Enemy_STANDBY)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                card.transform.rotation = Quaternion.identity;
            }
        }
    }

    void MainPhase()
    {
        Debug.Log("MainPhase");
        // フェーズ変更に伴うテキストの更新
        UpdatePhaseText();
        Summonable = true;
        phase_text.GetComponent<TextMeshProUGUI>().text = currentPlayer + "\nMain";
    }
    void BattlePhase()
    {
        Debug.Log("BattlePhase");
        // フェーズ変更に伴うテキストの更新
        UpdatePhaseText();
        Movable = false;
        Summonable = false;
        if(turn != 0)
        {
            Attackable = true;
        }

        phase_text.GetComponent<TextMeshProUGUI>().text = currentPlayer + "\nBattle";

        // BATTLEフェーズに入ったのでクリックを許可する
        foreach (var objClickExample in FindObjectsOfType<ObjectClickExample>())
        {
            objClickExample.EnterBattlePhase();
        }

    }
    void EndPhase()
    {
        Debug.Log("EndPhase");
        E_draw = true;
        // フェーズ変更に伴うテキストの更新
        UpdatePhaseText();
        Attackable = false;
        phase_text.GetComponent<TextMeshProUGUI>().text = currentPlayer + "\nEnd";
        for (int i = 0; i < playerFieldCardList.Length; i++)
        {
            playerFieldCardList[i].power_back();
        }
        //if (currentPlayer == playerList[0])
        //{
        //    currentPlayer = playerList[1];
        //}
        //else
        //{
        //    currentPlayer = playerList[0];
        //}
        Invoke("changeP_end", 3.5f);

        // BATTLEフェーズから出たのでクリックを禁止する
        foreach (var objClickExample in FindObjectsOfType<ObjectClickExample>())
        {
            objClickExample.ExitBattlePhase();
        }
    }

    void changeP_end()
    {
        
        phase = Phase.Enemy_DRAW;
        
    }

    void Enemy_DrawPhase()
    {
        Debug.Log("Enemy_DrawPhase");
        
        // フェーズ変更に伴うテキストの更新
        UpdatePhaseText();
        phase_text.GetComponent<TextMeshProUGUI>().text = "Enemy" + "\nDraw";
        currentPlayer.EnemyDraw();
        Invoke("Invoke_changeStanby", 3.5f);
    }

    void Invoke_changeStanby()
    {
        phase = Phase.Enemy_STANDBY;
    }

    void Enemy_StandbyPhase()
    {
        Debug.Log("Enemy_StandbyPhase");
        // フェーズ変更に伴うテキストの更新
        UpdatePhaseText();
        phase_text.GetComponent<TextMeshProUGUI>().text = "Enemy" + "\nStandby";
        _cpu.Standby();
        Invoke("Invoke_changeMain", 3.5f);
    }

    void Invoke_changeMain()
    {
        phase = Phase.Enemy_MAIN;
    }

    void Enemy_MainPhase()
    {
        if(main)
        {
            Debug.Log("Enemy_MainPhase");
            // フェーズ変更に伴うテキストの更新
            UpdatePhaseText();
            phase_text.GetComponent<TextMeshProUGUI>().text = "Enemy" + "\nMain";
            //cpu_script.Main(turn);
            cpu_script.Main();
            main = false;
        }
        //phase = Phase.Enemy_BATTLE;
    }

    void Enemy_BattlePhase()
    {
        if(battle)
        {
            Debug.Log("Enemy_BattlePhase");
            // フェーズ変更に伴うテキストの更新
            UpdatePhaseText();
            phase_text.GetComponent<TextMeshProUGUI>().text = "Enemy" + "\nBattle";
            main = true;
            battle = false;
            cpu_script.battle(turn);
        }

        if (manage_script.Stop)
        {
            phase = Phase.Enemy_END;
        }
        Blockable = true;
    }

    void Enemy_EndPhase()
    {
        Debug.Log("Enemy_EndPhase");
        // フェーズ変更に伴うテキストの更新
        UpdatePhaseText();
        phase_text.GetComponent<TextMeshProUGUI>().text = "Enemy" + "\nEnd";        
        battle = true;
        //Invoke("Invoke_draw", 3.5f);
        phase = Phase.DRAW;
        Blockable = false;
    }

    void Invoke_draw()
    {
        Debug.Log("Invoke後どろーふぇーず");

        phase = Phase.DRAW;
        Debug.Log(phase);
    }

    public void Change_Main()
    {
        phase = Phase.Enemy_MAIN;
    }

    public void Change_Battle()
    {
        phase = Phase.Enemy_BATTLE;
    }

    public void Change_End()
    {
        phase = Phase.Enemy_END;

    }

    

    public void NextPhase()
    {
        switch (phase)
        {
            case Phase.STANDBY://スタンバイ（移動）フェーズ
                phase = Phase.MAIN;
                break;
            case Phase.MAIN://メインフェーズ
                phase = Phase.BATTLE;
                break;
            case Phase.BATTLE://バトルフェーズ
                phase = Phase.END;
                break;
        }
    }

    public void ChangePhase(Phase newPhase)
    {
        phase = newPhase;
        // phaseIndexパラメータを更新
        if (phaseAnimator != null)
        {
            phaseAnimator.SetInteger("phaseIndex", (int)phase);
        }
        UpdatePhaseText(); // フェーズテキストの更新
        TriggerPhaseAnimation(); // フェーズアニメーションのトリガー
    }

    void TriggerPhaseAnimation()
{
    // アニメーションのトリガー名を 'Phase' に合わせてトリガーする
    phaseAnimator.SetTrigger("Phase");
}



    void UpdatePhaseText()
    {
        Color enemyPhaseColor = new Color(255f / 255f, 14f / 255f, 14f / 255f,255f / 255f); // 敵のフェーズの色を赤に設定
        Color playerPhaseColor = new Color(55f / 255f, 140f / 255f, 212f / 255f, 255f / 255f); // プレイヤーのフェーズの色を青に設定
        switch (phase)
        {
            // プレイヤーフェーズの場合、テキストの色を青色に設定
            case Phase.DRAW:
                phaseText.text = "Draw Phase";
                phaseText.color = playerPhaseColor; // 青色
                break;
            case Phase.STANDBY:
                phaseText.text = "Standby Phase";
                phaseText.color = playerPhaseColor; // 青色
                break;
            case Phase.MAIN:
                phaseText.text = "Main Phase";
                phaseText.color = playerPhaseColor;  // 青色
                break;
            case Phase.BATTLE:
                phaseText.text = "Battle Phase";
                phaseText.color = playerPhaseColor;  // 青色
                break;
            case Phase.END:
                phaseText.text = "End Phase";
                phaseText.color = playerPhaseColor;  // 青色
                break;

            // 敵のフェーズの場合、テキストの色を赤色に設定
            case Phase.Enemy_DRAW:
                phaseText.text = "Draw Phase";
                phaseText.color = enemyPhaseColor; // 赤色
                break;
            case Phase.Enemy_STANDBY:
                phaseText.text = "Standby Phase";
                phaseText.color = enemyPhaseColor; // 赤色
                break;
            case Phase.Enemy_MAIN:
                phaseText.text = "Main Phase";
                phaseText.color = enemyPhaseColor; // 赤色
                break;
            case Phase.Enemy_BATTLE:
                phaseText.text = "Battle Phase";
                phaseText.color = enemyPhaseColor; // 赤色
                break;
            case Phase.Enemy_END:
                phaseText.text = "End Phase";
                phaseText.color = enemyPhaseColor; // 赤色
                break;
            default:
                phaseText.text = "Undefined Phase";
                phaseText.color = Color.white; // デフォルトの色
                break;
        }
    }

    IEnumerator Destroy_me(GameObject me)
    {
        SE.hakai_SE();
        yield return new WaitForSeconds(0.5f);
        Destroy(me);
    }

    IEnumerator Destroy_me_ETurn(GameObject me)
    {
        SE.hakai_SE();
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Destroy_me_ETrunの呼び出し");
        Destroy(me);
        Debug.Log("ですとろい");
        Invoke("E_judge", 0.1f);
    }

    void E_judge()
    {
        _cpu.EnemyAttackJudge();
    }

    public void Ensyutu_Start()
    {
        Ensyutu.SetActive(true);
    }

    public void Ensyutu_End()
    {
        Ensyutu.SetActive(false);
    }

    public void Battle(GameObject attack,GameObject block)
    {
        int attack_power = attack.GetComponent<CardView>().power;
        int block_power = block.GetComponent<CardView>().power;
        Debug.Log("バトル開始");
        Debug.Log("アタックパワー" + attack_power);
        Debug.Log("ブロックパワー" + block_power);
        if (phase == Phase.Enemy_BATTLE)
        {
            Debug.Log("エネミーバトルフェイズ");
            if (attack_power > block_power)
            {
                Debug.Log("アタッカーの勝ち");
                StartCoroutine(Destroy_me_ETurn(block));
            }
            else if (attack_power < block_power)
            {
                Debug.Log("ブロッカーの勝ち");
                _cpu.AtkCnt -= 1;
                StartCoroutine(Destroy_me_ETurn(attack));
            }
            else if (attack_power == block_power)
            {
                Debug.Log("引き分け");
                _cpu.AtkCnt -= 1;
                StartCoroutine(Destroy_me(attack));
                StartCoroutine(Destroy_me_ETurn(block));
            }
        }
        else
        {
            if (attack_power > block_power)
            {
                Debug.Log("アタッカーの勝ち");
                StartCoroutine(Destroy_me(block));
            }
            else if (attack_power < block_power)
            {
                Debug.Log("ブロッカーの勝ち");
                StartCoroutine(Destroy_me(attack));
            }
            else if (attack_power == block_power)
            {
                Debug.Log("引き分け");
                StartCoroutine(Destroy_me(attack));
                StartCoroutine(Destroy_me(block));
            }
        }

        attack.GetComponent<CardController>().attack = false;
        block.GetComponent<CardController>().block = false;
        playerattack = false;
        enemyattack = false;
    }

    IEnumerator Color_FadeOut()
    {
        fade_panel.SetActive(true);
        // 画面をフェードアウトさせるコールチン
        // 前提：画面を覆うPanelにアタッチしている

        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = fade_panel.GetComponent<Image>();

        // フェード後の色を設定（黒）★変更可
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 255.0f));

        // フェードインにかかる時間（秒）★変更可
        const float fade_time = 0.3f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 10;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        // Color_FadeOut 完了後、1秒待ってから Color_FadeIn 実行
        yield return new WaitForSeconds(1f);
        if(enemy_life <= 0)
        {
            Win();
        }
        else if (player_life >= 0)
        {
            Lose();
        }
    }

    private void Win()
    {
        SceneManager.LoadScene("WinScene");
    }

    private void Lose()
    {
        SceneManager.LoadScene("LoseScene");
    }
}
