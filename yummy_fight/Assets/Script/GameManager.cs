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
    //�����^�[���P�����p�ϐ�
    public bool Buns, Patty,Muffin,Pickles,Foodraw,Plan,Stop, bagamute, egumahu, torabaga, chibaga;

    bool isPlayerTurn = true; //
    public List<int> deck = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2 };
    public List<int> Edeck = new List<int>() { 5, 5, 5, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2 };//

    public GameDirecter _directer;
    void Awake()
    {
        // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���PhotonView�R���|�[�l���g���擾
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        Shuffle(deck);
        Shuffle(Edeck);
        SE = GameObject.Find("SE").GetComponent<SE_Controller>();
        Menu.SetActive(false);
        // �V�[���̎���������L���ɂ���
        PhotonNetwork.AutomaticallySyncScene = true;
        StartGame();
        // �V�[��������Ԃ̃`�F�b�N���J�n
        StartCoroutine(CheckSceneSyncStatus());
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
    }

    void Update()
    {
        // parentObject �̂��ׂĂ̎q�I�u�W�F�N�g���폜
        foreach (Transform child in enemyHandUra)
        {
            GameObject.Destroy(child.gameObject);
        }

        for(int i = 0;i<_directer.enemyHandCardList.Length;i++)
        {
            CreateHandUra(999, enemyHandUra);
        }
    }

     public void Shuffle(List<int> deck) // �f�b�L���V���b�t������
    {
        // ���� n �̏����l�̓f�b�L�̖���
        int n = deck.Count;

        // n��1��菬�����Ȃ�܂ŌJ��Ԃ�
        while (n > 1)
        {
            n--;

            // k�� 0 �` n+1 �̊Ԃ̃����_���Ȓl
            int k = UnityEngine.Random.Range(0, n + 1);

            // k�Ԗڂ̃J�[�h��temp�ɑ��
            int temp = deck[k];
            deck[k] = deck[n];
            deck[n] = temp;
        }
    }
    public void Shuffle() // �f�b�L���V���b�t������
    {
        // ���� n �̏����l�̓f�b�L�̖���
        int n = deck.Count;

        // n��1��菬�����Ȃ�܂ŌJ��Ԃ�
        while (n > 1)
        {
            n--;

            // k�� 0 �` n+1 �̊Ԃ̃����_���Ȓl
            int k = UnityEngine.Random.Range(0, n + 1);

            // k�Ԗڂ̃J�[�h��temp�ɑ��
            int temp = deck[k];
            deck[k] = deck[n];
            deck[n] = temp;
        }
    }

    void StartGame() // �����l�̐ݒ� 
    {
        // ������D��z��
        SetStartHand();

        
        // �^�[���̌���
        TurnCalc();
    }
    IEnumerator CheckSceneSyncStatus()
    {
        while (true)
        {
            Debug.Log("�V�[������������Ă���ꍇ�͓�������Ă��܂�: " + PhotonNetwork.AutomaticallySyncScene);
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
                Debug.LogError("�s���ȏꏊ: " + placeName);
                return;
        }

        CreateCard(cardID, placeTransform);
    }

    public void SummonCard(int cardID)
    {
        // ���[�J���v���C���[�̃t�B�[���h�ɃJ�[�h������
        CreateCard(cardID, playerField);

        // �����[�g�v���C���[�̃t�B�[���h�ɂ��J�[�h��\�����邽�߂�RPC���Ăяo��
        photonView.RPC("CreateCardNetwork", RpcTarget.Others, cardID, "enemyField");
    }


    public void DrawCard(Transform hand) // �J�[�h������
    {
        // �f�b�L���Ȃ��Ȃ�����Ȃ�
        if (deck.Count == 0)
        {
            return;
        }
        
        // �f�b�L�̈�ԏ�̃J�[�h�𔲂����A��D�ɉ�����
        SE.draw_SE();
        int cardID = deck[0];
        deck.RemoveAt(0);
        Debug.Log("�h���[�I");
        CreateCard(cardID, hand);  
    }

    public void EnemyDraw(Transform hand)
    {
        // �f�b�L���Ȃ��Ȃ�����Ȃ�
        if (Edeck.Count == 0)
        {
            return;
        }
        if (_directer.E_draw)
        {
            // �f�b�L�̈�ԏ�̃J�[�h�𔲂����A��D�ɉ�����
            SE.draw_SE();
            int cardID = Edeck[0];
            Edeck.RemoveAt(0);
            CreateCard(cardID, hand);
            Debug.Log("�G�l�~�[�h���[");
            _directer.E_draw = false;
        }

    }
    public void EnemyDraw_Start(Transform hand)
    {
        // �f�b�L���Ȃ��Ȃ�����Ȃ�
        if (Edeck.Count == 0)
        {
            return;
        }
        
        // �f�b�L�̈�ԏ�̃J�[�h�𔲂����A��D�ɉ�����
        SE.draw_SE();
        int cardID = Edeck[0];
        Edeck.RemoveAt(0);
        CreateCard(cardID, hand);
        Debug.Log("�G�l�~�[�h���[");

        
    }

    void SetStartHand() // ��D��5���z��
    {
        for (int i = 0; i < 5; i++)
        {
            DrawCard(playerHand);
            EnemyDraw_Start(enemyHand);
            //CreateCard(999, enemyHand);
        }
    }

    void TurnCalc() // �^�[�����Ǘ�����
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

    public void ChangeTurn() // �^�[���G���h�{�^���ɂ��鏈��
    {
        isPlayerTurn = !isPlayerTurn; // �^�[�����t�ɂ���
        TurnCalc(); // �^�[���𑊎�ɉ�
        Buns = false;
        Patty = false;
        Muffin = false;
        Pickles = false;
    }

    void PlayerTurn()
    {
        Debug.Log("Player�̃^�[��");

        DrawCard(playerHand); // ��D���ꖇ������
    }

    void EnemyTurn()
    {
        Debug.Log("Enemy�̃^�[��");

        //CreateCard(1, enemyField); // �J�[�h������

        ChangeTurn(); // �^�[���G���h����
    }
}