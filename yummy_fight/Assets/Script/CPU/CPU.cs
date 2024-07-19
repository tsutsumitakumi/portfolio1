using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU : MonoBehaviour
{
    // Start is called before the first frame update
    public GameDirecter _directer;
    public GameManager _manager;
    public CardController _Controller;
    public GameObject hand;
    int max = 0;
    public int AtkCnt = 0;
    int hirouCnt = 0;
    public AttackButton _AttackButton;
    int[] array;
    public bool bans;
    public bool mafin;
    public bool patty;
    //int serach = 0;             //カード効果で使うドロー変数

    void Start()
    {
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _Controller = GameObject.Find("CardController").GetComponent<CardController>();        
        hand = GameObject.Find("Canvas/enemy_hand");
    }

    //バガムート　101 素材　バンズ 1 OR マフィン　3 と　パティ　2　と　ピクルス　4 と　レタス　6　と　トマト　8
    //チーバガ　104 素材　バンズ 1 OR マフィン　3　と　パティ　2　と チーズ　5
    //トレバガ　103 素材　バンズ 1 OR マフィン　3　と　パティ　2　と　トマト　8
    //エグマフ　102 素材　マフィン　3 と　パティ　2　と エッグ　7
    //ハーフ　105 素材　バンズ 1 OR マフィン　3 と　パティ　2
    // Update is called once per frame
    void Update()
    {
        
        if (_directer.playerattack && !_directer.Koukahatudou && _directer.EnemyFieldCardList.Length > 0 && !_directer.enemyblock)
        {
            hirouCnt = 0;
            //max = 0;
            //_AttackButton = GameObject.Find("Attack").GetComponent<AttackButton>();
            Debug.Log("プレイヤーの攻撃を検知");
            if(!_directer.Koukahatudou && _directer.EnemyFieldCardList.Length > 0) //
            {
                Debug.Log("Koukahatudouとかに引っかかった");
                for (int i = 0; i < _directer.EnemyFieldCardList.Length; i++)
                {
                    if (_directer.EnemyFieldCardList[i].hirou)
                    {
                        hirouCnt++;
                    }
                }
                Debug.Log("hirouCnt " + hirouCnt);
                if((_directer.EnemyFieldCardList.Length <= hirouCnt) && _directer.playerattack)     //enemyFieldがすべて疲労状態なら
                {
                    //_AttackButton.cardObject.GetComponent<CardController>().attack = false;
                    for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
                    {
                        _directer.playerFieldCardList[i].attack = false;
                    }
                    _directer.playerattack = false;
                    _directer.enemy_life--;
                    
                    Debug.Log("ブロックできないよ");
                    Debug.Log(_directer.playerattack);
                    
                }
                else
                {
                    _directer.enemyblock = true;
                    StartCoroutine("Block");
                    Debug.Log("Blockコルーチン");
                }
            }
        }

        array = new int[_directer.enemyHandCardList.Length];
        for (int i = 0; i < _directer.enemyHandCardList.Length; i++)  //int型の配列にenemyの手札カードのIDを保存
        {
            array[i] = _directer.enemyHandCardList[i].view.cardID;
        }
    }

    public IEnumerator Block()      
    {
        yield return new WaitForSeconds(0);
        Debug.Log("Blockコルーチン実行");
        if (_directer.EnemyFieldCardList.Length > 0)    //CPUのフィールドに1体以上モンスターがいるとき
        {
            for (int i = 0; i < _directer.EnemyFieldCardList.Length; i++)
            {
                if (max < _directer.EnemyFieldCardList[i].GetComponent<CardView>().power && _directer.EnemyFieldCardList[i].hirou == false)   //もっともパワーが高いやつを探す
                {
                    max = _directer.EnemyFieldCardList[i].GetComponent<CardView>().power;   //入れる
                }
            }
            Debug.Log("max" + max);
            for (int i = 0; i < _directer.EnemyFieldCardList.Length; i++)
            {
                if (max == _directer.EnemyFieldCardList[i].GetComponent<CardView>().power && _directer.EnemyFieldCardList[i].hirou == false)  //一番パワーでかいやつを探す
                {
                    _directer.EnemyFieldCardList[i].enemyblock();   //おったらそいつでブロック
                    _directer.playerattack = false;
                    break;
                }
                
            }
        }
        _directer.enemyblock = false;
        max = 0;
    }

    public void Standby()
    {
        for(int i = 0; i< _directer.EnemyKitchenCardList.Length;i++)
        {
            if (_directer.EnemyFieldCardList.Length <= 1)
            {
                _directer.EnemyKitchenCardList[i].gameObject.transform.SetParent(_manager.enemyField);
                _directer.EnemyKitchenCardList = _manager.enemyKitchen.GetComponentsInChildren<CardController>();
                _directer.EnemyFieldCardList = _manager.enemyField.GetComponentsInChildren<CardController>();
            }
            
        }
        for (int i = 0; i < _directer.EnemyFieldCardList.Length; i++)
        {
            _directer.EnemyFieldCardList[i].kaihuku_Enemy();
            _directer.EnemyFieldCardList[i].hirou = false;
        }
        bans = false;
        mafin = false;
        patty = false;
        AtkCnt = 0;
    }

    public void Main()
    {
        //バガムート　101 素材　バンズ 1 OR マフィン　3 と　パティ　2　と　ピクルス　4 と　レタス　6　と　トマト　8
        //チーバガ　104 素材　バンズ 1 OR マフィン　3　と　パティ　2　と チーズ　5
        //トレバガ　103 素材　バンズ 1 OR マフィン　3　と　パティ　2　と　トマト　8
        //エグマフ　102 素材　マフィン　3 と　パティ　2　と エッグ　7
        //ハーフ　105 素材　バンズ 1 OR マフィン　3 と　パティ　2


        /*array = new int[_directer.enemyHandCardList.Length];
        for (int i = 0; i < _directer.enemyHandCardList.Length; i++)  //int型の配列にenemyの手札カードのIDを保存
        {
            array[i] = _directer.enemyHandCardList[i].view.cardID;
            Debug.Log(array[i]);
        }*/

        if (_directer.EnemyFieldCardList.Length <= 2)       //フィールドに出せるカードの制限
        {
            
            HandCheck();
            Tyouri();
            Debug.Log("チェック１_directer.EnemyKitchenCardList.Length　" + _directer.EnemyKitchenCardList.Length);
        }
        StartCoroutine(Change_main(7));                            //メインターン終了    
        
    }
    public void kouka(int serach)
    {
        switch (serach)
        {
            case 1:
                if (!bans)
                {
                    _manager.CreateCard(2, hand.transform);   //バンズの能力を発動
                    bans = true;
                }                
                break;

            case 2:
                if (!patty)
                {
                    _manager.CreateCard(1, hand.transform);   //パティの能力を発動
                    patty = true;
                }
                break;
            case 3:
                if (!mafin)
                {
                    _manager.CreateCard(2, hand.transform);   //マフィンの能力を発動
                    mafin = true;
                }
                break;

            default:
                break;
        }
    }

    void Tyouri()
    {
        StartCoroutine(ChiBaga(0.5f));
        StartCoroutine(Harf(1.0f));
    }

    IEnumerator Harf(float wait)
    {
        yield return new WaitForSeconds(wait);
        _directer.EnemyKitchenCardList = _manager.enemyKitchen.GetComponentsInChildren<CardController>();
        for (int a = 0; a < _directer.EnemyKitchenCardList.Length; a++)    //キッチンエリアをみて
        {

            if (_directer.EnemyKitchenCardList[a].view.cardID == 1 || _directer.EnemyKitchenCardList[a].view.cardID == 3)        //バンズがあるときかマフィンがあるとき
            {

                for (int b = 0; b < _directer.EnemyKitchenCardList.Length; b++)
                {
                    if (_directer.EnemyKitchenCardList[b].view.cardID == 2)  //パティがあるとき
                    {
                        /*StartCoroutine(Create(array[a], _manager.enemyKitchen, 1));//バンズorマフィン
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Debug.Log(_directer.enemyHandCardList[a].view.cardID);
                        StartCoroutine(Create(array[b], _manager.enemyKitchen, 2));//パティ
                        kouka(_directer.enemyHandCardList[b].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        Destroy(_directer.enemyHandCardList[b].gameObject);*/
                        //StartCoroutine(Create(1, _manager.enemyField, 3));
                        YugouDestroy(105);
                        StartCoroutine(Yugou(105, _manager.enemyField, 0.2f));        //半バーガー召喚
                        Debug.Log("半バーガー");
                        break;
                    }
                    
                }
                break;
            }

        }
    }

    void Bagamu()
    {
        
        for (int a = 0; a < _directer.EnemyKitchenCardList.Length; a++)    //キッチンをみて
        {

            if (_directer.EnemyKitchenCardList[a].view.cardID == 8)        //トマトがあるとき
            {
                for (int b = 0; b < _directer.EnemyKitchenCardList.Length; b++)
                {
                    if (_directer.EnemyKitchenCardList[b].view.cardID == 6)  //レタスがあるとき
                    {
                        for (int c = 0; c < _directer.EnemyKitchenCardList.Length; c++)
                        {
                            if (_directer.EnemyKitchenCardList[c].view.cardID == 4)     //ピクルスがあるとき
                            {
                                for (int d = 0; d < _directer.EnemyKitchenCardList.Length; d++)
                                {
                                    if (_directer.EnemyKitchenCardList[d].view.cardID == 1 || _directer.EnemyKitchenCardList[d].view.cardID == 3) //バンズかマフィンがあるとき
                                    {
                                        StartCoroutine(Yugou(101, _manager.enemyField, 3));
                                    }
                                }
                            }
                        }

                    }
                }
            }

        }
    }

    IEnumerator ChiBaga(float wait)
    {
        yield return new WaitForSeconds(wait);
        _directer.EnemyKitchenCardList = _manager.enemyKitchen.GetComponentsInChildren<CardController>();
        for (int a = 0; a < _directer.EnemyKitchenCardList.Length; a++)    //キッチンエリアをみて
        {

            if (_directer.EnemyKitchenCardList[a].view.cardID == 1 || _directer.EnemyKitchenCardList[a].view.cardID == 3 && !bans && !mafin)        //バンズがあるときかマフィンがあるとき
            {

                for (int b = 0; b < _directer.EnemyKitchenCardList.Length; b++)
                {
                    if (_directer.EnemyKitchenCardList[b].view.cardID == 2)  //パティがあるとき
                    {
                        for(int c = 0; c < _directer.EnemyKitchenCardList.Length; c++)
                        {
                            if (_directer.EnemyKitchenCardList[c].view.cardID == 5) //チーズがあるとき
                            {
                                YugouDestroy(104);
                                StartCoroutine(Yugou(104, _manager.enemyField, 0.2f));        //チーバガ召喚

                                break;
                            }
                        }
                        break;
                    }
                }
                break;
            }
            

        }
    }

    void HandCheck()
    {
        for(int a = 0; a < _directer.enemyHandCardList.Length; a++)
        {
            Debug.Log("_directer.EnemyKitchenCardList.Length" + _directer.EnemyKitchenCardList.Length);
            if(_directer.EnemyKitchenCardList.Length < 5)
            {
                //StartCoroutine(wait(a, 1));
                switch (_directer.enemyHandCardList[a].view.cardID)
                {
                    case 1:
                        Create1(1, _manager.enemyKitchen, 1);//バンズ
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 2:
                        Create1(2, _manager.enemyKitchen, 1);//パティ
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 3:
                        Create1(3, _manager.enemyKitchen, 1);//マフィン
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 4:
                        Create1(4, _manager.enemyKitchen, 1);//ピクルス
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 5:
                        Create1(5, _manager.enemyKitchen, 1);//チーズ
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 6:
                        Create1(6, _manager.enemyKitchen, 1);//レタス
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 7:
                        Create1(7, _manager.enemyKitchen, 1);//エッグ
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 8:
                        Create1(8, _manager.enemyKitchen, 1);//トマト
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                }
                Debug.Log("チェック_directer.EnemyKitchenCardList.Length　" + _directer.EnemyKitchenCardList.Length);
            }
            

        }
    }

    IEnumerator wait(int a,int wait)
    {
        Debug.Log("wait");
        yield return new WaitForSeconds(wait);
        switch (_directer.enemyHandCardList[a].view.cardID)
        {
            case 1:
                Create1(array[a], _manager.enemyKitchen, 1);//バンズ
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 2:
                Create1(array[a], _manager.enemyKitchen, 1);//パティ
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 3:
                Create1(array[a], _manager.enemyKitchen, 1);//マフィン
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 4:
                Create1(array[a], _manager.enemyKitchen, 1);//ピクルス
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 5:
                Create1(array[a], _manager.enemyKitchen, 1);//チーズ
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 6:
                Create1(array[a], _manager.enemyKitchen, 1);//レタス
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 7:
                Create1(array[a], _manager.enemyKitchen, 1);//エッグ
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 8:
                Create1(array[a], _manager.enemyKitchen, 1);//トマト
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
        }
    }

    public void battle(int turn)
    {
        
        
        if(_directer.EnemyFieldCardList.Length > 0) //CPUのフィールドにカードが一枚以上あるとき
        {          
            EnemyAttackJudge();
        }
        else
        {
            _directer.Change_End();
        }
        
        
        /*switch(turn)
        {
            case 1:
                _directer.Change_End();
                break;
                
            case 2:
                for(int i=0;i<_directer.EnemyFieldCardList.Length;i++)
                {
                    if(_directer.EnemyFieldCardList[i].gameObject.GetComponent<CardView>().cardID == 102)
                    {
                        _directer.EnemyFieldCardList[i].enemyattack();
                    }
                }
                break;
        }*/
    }

    public void EnemyAttackJudge()
    {
        Debug.Log("EnemyAttackJudgeの実行");
        
        if (AtkCnt >= _directer.EnemyFieldCardList.Length || _directer.EnemyFieldCardList.Length <= 0)
        {
            Debug.Log("エンドフェイズ突入" + AtkCnt);
            _directer.Change_End();
            AtkCnt = 0;
        }

        int P_maxPower = 0; //プレイヤーの最もパワーの高いカードの番地
        if(_directer.playerFieldCardList.Length <= 0)
        {
            //P_maxPower = 100;
        }
        else
        {
            for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
            {

                if (_directer.playerFieldCardList[i].hirou)
                {
                    hirouCnt++;
                    P_maxPower++;

                    continue;
                }

                if(P_maxPower == i) 
                {
                    continue;
                }
                if ((_directer.playerFieldCardList[P_maxPower].view.power < _directer.playerFieldCardList[i].view.power) && _directer.playerFieldCardList[i].hirou == false) //攻撃力が最も高いカードを探す
                {
                    P_maxPower = i;   //登録                
                }
            }
            
            max = 0;
        }

        int maxPower = 0;       //最もパワーの高いカードの番地を格納する

        
        for (int i = 0; i < _directer.EnemyFieldCardList.Length; i++)   //CPUのフィールドのカードを見ていく
        {
            if (_directer.EnemyFieldCardList[maxPower].hirou)
            {
                maxPower++;
                
                continue;
            }
            if ((_directer.EnemyFieldCardList[maxPower].view.power < _directer.EnemyFieldCardList[i].view.power) && _directer.EnemyFieldCardList[i].hirou == false) //攻撃力が最も高いカードを探す
            {
                maxPower = i;   //登録                
            }

        }


        if (!_directer.EnemyFieldCardList[maxPower].hirou)    //カードが疲労状態じゃないなら攻撃
        {
            if(_directer.playerFieldCardList.Length == 0)   //プレイヤーフィールドが０のとき
            {
                Debug.Log("レイヤーフィールドが０のとき");
                _directer.EnemyFieldCardList[maxPower].enemyattack();
                _directer.EnemyFieldCardList[maxPower].hirou = true;
                AtkCnt++;
                P_maxPower = 0;
                Debug.Log("AtkCnt" + AtkCnt);
            }
            else if (_directer.playerFieldCardList.Length == hirouCnt)   //プレイヤーフィールドが全て疲労状態のとき
            {
                Debug.Log("プレイヤーフィールドが全て疲労状態のとき");
                _directer.EnemyFieldCardList[maxPower].enemyattack();
                _directer.EnemyFieldCardList[maxPower].hirou = true;
                AtkCnt++;
                P_maxPower = 0;
                Debug.Log("AtkCnt" + AtkCnt);
            }
            else if (_directer.playerFieldCardList[P_maxPower].view.power　<= _directer.EnemyFieldCardList[maxPower].view.power) //アタックして相打ち以上のとき
            {
                Debug.Log("アタックして相打ち以上のとき");
                _directer.EnemyFieldCardList[maxPower].enemyattack();
                _directer.EnemyFieldCardList[maxPower].hirou = true;
                AtkCnt++;
                Debug.Log("AtkCnt" + AtkCnt);
                maxPower = 0;
                P_maxPower = 0;
            }
            else if(_directer.playerFieldCardList[P_maxPower].view.power > _directer.EnemyFieldCardList[maxPower].view.power)
            {
                Debug.Log("maxPower" + maxPower);
                Debug.Log("P_maxPower" + P_maxPower);
                Debug.Log("_directer.EnemyFieldCardList[maxPower].view.power" + _directer.EnemyFieldCardList[maxPower].view.power);
                Debug.Log("_directer.playerFieldCardList[P_maxPower].view.power" + _directer.playerFieldCardList[P_maxPower].view.power);
                Debug.Log("_directer.playerFieldCardList[P_maxPower].view.hirou" + _directer.playerFieldCardList[P_maxPower].hirou);
                Debug.Log("↓");
                maxPower = 0;
                P_maxPower = 0;
                _directer.Change_End();
            }
            
            
        }
        else
        {
            Debug.Log("何も... ながっだ...");
            AtkCnt = 0;
            _directer.Change_End();

        }
        maxPower = 0;
        hirouCnt = 0;
    }

    IEnumerator Create(int id,Transform place, int wait)
    {
        yield return new WaitForSeconds(wait);
        _manager.CreateCard(id, place);
    }

    void Create1(int id, Transform place, int wait)
    {
        _manager.CreateCard(id, place);
        _directer.EnemyKitchenCardList = _manager.enemyKitchen.GetComponentsInChildren<CardController>();
        Debug.Log("えねみーきっちん:" + _directer.EnemyKitchenCardList.Length);
    }

    void YugouDestroy(int id)
    {
        switch (id)
        {
            case 105:
                for (int a = 0; a < _directer.EnemyKitchenCardList.Length; a++)
                {
                    if (_directer.EnemyKitchenCardList[a].view.cardID == 1 || _directer.EnemyKitchenCardList[a].view.cardID == 3)
                    {
                        Destroy(_directer.EnemyKitchenCardList[a].gameObject);
                        for (int b = 0; b < _directer.EnemyKitchenCardList.Length; b++)
                        {
                            if (_directer.EnemyKitchenCardList[b].view.cardID == 2)
                            {
                                Destroy(_directer.EnemyKitchenCardList[b].gameObject);
                                break;
                            }
                        }
                        break;
                    }

                }
                break;

            case 104:
                for (int a = 0; a < _directer.EnemyKitchenCardList.Length; a++)
                {
                    if (_directer.EnemyKitchenCardList[a].view.cardID == 1 || _directer.EnemyKitchenCardList[a].view.cardID == 3)
                    {
                        Destroy(_directer.EnemyKitchenCardList[a].gameObject);
                        for (int b = 0; b < _directer.EnemyKitchenCardList.Length; b++)
                        {
                            if (_directer.EnemyKitchenCardList[b].view.cardID == 2)
                            {
                                Destroy(_directer.EnemyKitchenCardList[b].gameObject);
                                for (int c = 0; c < _directer.EnemyKitchenCardList.Length; c++)
                                {
                                    if (_directer.EnemyKitchenCardList[c].view.cardID == 5)
                                    {
                                        Destroy(_directer.EnemyKitchenCardList[c].gameObject);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }

                }
                break;

        }
        _directer.EnemyKitchenCardList = _manager.enemyKitchen.GetComponentsInChildren<CardController>();
        _directer.EnemyFieldCardList = _manager.enemyField.GetComponentsInChildren<CardController>();
    }
    IEnumerator Yugou(int id, Transform place, float wait)
    {   
        yield return new WaitForSeconds(wait);
        /*for (int i = 0; i < _directer.EnemyKitchenCardList.Length; i++)
        {
            Destroy(_directer.EnemyKitchenCardList[i].gameObject);
        }*/
        _manager.CreateCard(id, place);
    }

    IEnumerator Change_main(int time)
    {
        yield return new WaitForSeconds(time);
        _directer.Change_Battle();
    }

    IEnumerator enemy_attack()
    {
        _directer.enemyattack = true;
        yield return new WaitForSeconds(0.1f);
    }
}
