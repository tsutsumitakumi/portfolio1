using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class GameManager : MonoBehaviour
{
    [SerializeField] CardController cardPrefab;
    [SerializeField] private HandCardsInfoSync handCardsInfoSync;
    public Transform playerHand, playerField,playerKitchen, enemyHand,enemyHandUra,enemyField,enemyKitchen,searchArea;
    public GameObject select_panel;
    private PhotonView photonView;
    SE_Controller SE;
    public GameObject Menu;
    //同名ターン１制限用変数
    public bool Buns, Patty,Muffin,Pickles,Foodraw,Plan,Stop, bagamute, egumahu, torabaga, chibaga;

    bool isPlayerTurn = true; //
    public List<int> deck = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2 };
    public List<int> Edeck = new List<int>() { 5, 5, 5, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2 };//

    public GameDirecter _directer;
    void Awake()
    {
        // このゲームオブジェクトにアタッチされているPhotonViewコンポーネントを取得
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        Shuffle(deck);
        Shuffle(Edeck);
        SE = GameObject.Find("SE").GetComponent<SE_Controller>();
        Menu.SetActive(false);
        // シーンの自動同期を有効にする
        PhotonNetwork.AutomaticallySyncScene = true;
        StartGame();
        // シーン同期状態のチェックを開始
        StartCoroutine(CheckSceneSyncStatus());
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
    }

    void Update()
    {
        // parentObject のすべての子オブジェクトを削除
        foreach (Transform child in enemyHandUra)
        {
            GameObject.Destroy(child.gameObject);
        }

        for(int i = 0;i<_directer.enemyHandCardList.Length;i++)
        {
            CreateHandUra(999, enemyHandUra);
        }
    }

     public void Shuffle(List<int> deck) // デッキをシャッフルする
    {
        // 整数 n の初期値はデッキの枚数
        int n = deck.Count;

        // nが1より小さくなるまで繰り返す
        while (n > 1)
        {
            n--;

            // kは 0 ～ n+1 の間のランダムな値
            int k = UnityEngine.Random.Range(0, n + 1);

            // k番目のカードをtempに代入
            int temp = deck[k];
            deck[k] = deck[n];
            deck[n] = temp;
        }
    }
    public void Shuffle() // デッキをシャッフルする
    {
        // 整数 n の初期値はデッキの枚数
        int n = deck.Count;

        // nが1より小さくなるまで繰り返す
        while (n > 1)
        {
            n--;

            // kは 0 ～ n+1 の間のランダムな値
            int k = UnityEngine.Random.Range(0, n + 1);

            // k番目のカードをtempに代入
            int temp = deck[k];
            deck[k] = deck[n];
            deck[n] = temp;
        }
    }

    void StartGame() // 初期値の設定 
    {
        // 初期手札を配る
        SetStartHand();

        
        // ターンの決定
        TurnCalc();
    }
    IEnumerator CheckSceneSyncStatus()
    {
        while (true)
        {
            Debug.Log("シーンが同期されている場合は同期されています: " + PhotonNetwork.AutomaticallySyncScene);
            yield return new WaitForSeconds(5f);
        }
    }

    public void CreateCard(int cardID, Transform place)
    {
        SE.draw_SE();
        CardController card = Instantiate(cardPrefab, place);
        card.Init(cardID);
    }

    public void CreateHandUra(int cardID, Transform place)
    {
        CardController card = Instantiate(cardPrefab, place);
        card.Init(cardID);
    }



    [PunRPC]
    public void CreateCardNetwork(int cardID, string placeName)
    {
        Transform placeTransform;
        switch (placeName)
        {
            case "playerField":
                placeTransform = playerField;
                break;
            case "enemyField":
                placeTransform = enemyField;
                break;
            default:
                Debug.LogError("不明な場所: " + placeName);
                return;
        }

        CreateCard(cardID, placeTransform);
    }

    public void SummonCard(int cardID)
    {
        // ローカルプレイヤーのフィールドにカードを召喚
        CreateCard(cardID, playerField);

        // リモートプレイヤーのフィールドにもカードを表示するためにRPCを呼び出す
        photonView.RPC("CreateCardNetwork", RpcTarget.Others, cardID, "enemyField");
    }


    public void DrawCard(Transform hand) // カードを引く
    {
        // デッキがないなら引かない
        if (deck.Count == 0)
        {
            return;
        }
        
        // デッキの一番上のカードを抜き取り、手札に加える
        SE.draw_SE();
        int cardID = deck[0];
        deck.RemoveAt(0);
        Debug.Log("ドロー！");
        CreateCard(cardID, hand);  
    }

    public void EnemyDraw(Transform hand)
    {
        // デッキがないなら引かない
        if (Edeck.Count == 0)
        {
            return;
        }
        if (_directer.E_draw)
        {
            // デッキの一番上のカードを抜き取り、手札に加える
            SE.draw_SE();
            int cardID = Edeck[0];
            Edeck.RemoveAt(0);
            CreateCard(cardID, hand);
            Debug.Log("エネミードロー");
            _directer.E_draw = false;
        }

    }
    public void EnemyDraw_Start(Transform hand)
    {
        // デッキがないなら引かない
        if (Edeck.Count == 0)
        {
            return;
        }
        
        // デッキの一番上のカードを抜き取り、手札に加える
        SE.draw_SE();
        int cardID = Edeck[0];
        Edeck.RemoveAt(0);
        CreateCard(cardID, hand);
        Debug.Log("エネミードロー");

        
    }

    void SetStartHand() // 手札を5枚配る
    {
        for (int i = 0; i < 5; i++)
        {
            DrawCard(playerHand);
            EnemyDraw_Start(enemyHand);
            //CreateCard(999, enemyHand);
        }
    }

    void TurnCalc() // ターンを管理する
    {
        if (isPlayerTurn)
        {
           //PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }

    public void ChangeTurn() // ターンエンドボタンにつける処理
    {
        isPlayerTurn = !isPlayerTurn; // ターンを逆にする
        TurnCalc(); // ターンを相手に回す
        Buns = false;
        Patty = false;
        Muffin = false;
        Pickles = false;
    }

    void PlayerTurn()
    {
        Debug.Log("Playerのターン");

        DrawCard(playerHand); // 手札を一枚加える
    }

    void EnemyTurn()
    {
        Debug.Log("Enemyのターン");

        //CreateCard(1, enemyField); // カードを召喚

        ChangeTurn(); // ターンエンドする
    }
}