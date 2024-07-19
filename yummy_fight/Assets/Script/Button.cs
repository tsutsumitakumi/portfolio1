using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameDirecter _directer;
    [SerializeField]
    private GameManager _manager;
    [SerializeField]
    private CardController _controller;
    public GameObject hand;
    public GameObject card;
    public GameObject menu;
    internal object onClick;
    SE_Controller SE;

    public int search_id;
    private int cnt;
    public Animator panel_anim;
    internal bool interactable;

    public CPU _cpu;
    public int BlockCard_ListNum;

    void Start()
    {
        SE = GameObject.Find("SE").GetComponent<SE_Controller>();
        hand = GameObject.Find("Player_hand");
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        card = transform.parent.gameObject;
        _cpu = GameObject.Find("GameDirecter").GetComponent<CPU>();
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }

    public void Menu_Show()
    {
        menu.SetActive(true);
    }

    public void Menu_Close()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Push()
    {
        _directer.NextPhase();
    }

    public void lifebreak()
    {
        SE.life_break_SE();
        _directer.player_life--;
        _directer.playerattack = false;
        _directer.enemyattack = false;
        this.gameObject.SetActive(false);
        for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
        {           
            _directer.playerFieldCardList[i].gameObject.GetComponent<CardController>().blockbutton.SetActive(false);            
        }
        _cpu.EnemyAttackJudge();
    }

    public void Block()
    {
        _controller.RotateCard();
        this.gameObject.SetActive(false);
        for(int i =0;i<_directer.EnemyFieldCardList.Length;i++)
        {
            if(_directer.EnemyFieldCardList[i].gameObject.GetComponent<CardController>().attack)
            {
                Debug.Log("アタッカー："+_directer.EnemyFieldCardList[i].gameObject);
                Debug.Log("ブロッカー：" + transform.parent.gameObject);
                _directer.Battle(_directer.EnemyFieldCardList[i].gameObject, card);
                _directer.life_de_ukeru.SetActive(false);
            }
        }
        for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
        {
            _directer.playerFieldCardList[i].gameObject.GetComponent<CardController>().blockbutton.SetActive(false);
        }

        if(card.GetComponent<CardView>().cardID == 103)
        {
            Debug.Log("トレバガ効果発動");
            card.GetComponent<EX_Card_Ability>().StartCoroutine("Torebaga");
        }
    }

    public void Kouka()
    {
        if(card.GetComponent<CardView>().cardID == 102)
        {
            card.GetComponent<EX_Card_Ability>().StartCoroutine("Egumahu");
            card.GetComponent<CardController>().egumahu_aru = false;
        }
    }

    public void Select()
    {
        if(_manager.chibaga)
        {
            for (int i = 0; i < _directer.SearchImageList.Length; i++)
            {
                if (_directer.SearchImageList[i].selected)
                {
                    _directer.playerFieldCardList[i].GetComponent<CardView>().power += 3000;
                    panel_anim.SetTrigger("Down");
                    _manager.chibaga = false;
                    _directer.Koukahatudou = false;
                    for (int t = 0; t < _directer.SearchImageList.Length; t++)
                    {
                        Destroy(_directer.SearchImageList[t].gameObject);
                    }
                }
            }
        }
        else if(_manager.egumahu)
        {
            for (int i = 0; i < _directer.SearchImageList.Length; i++)
            {
                if (_directer.SearchImageList[i].selected)
                {
                    for(int t = 0;t<_directer.playerFieldCardList.Length;t++)
                    {
                        if (_directer.SearchImageList[i].GetComponent<Search_id>().id == _directer.playerFieldCardList[t].gameObject.GetComponent<CardView>().cardID)
                        {
                            _directer.playerFieldCardList[t].gameObject.GetComponent<CardController>().kaihuku();
                        }
                    }
                    panel_anim.SetTrigger("Down");
                    _manager.egumahu = false;
                    _directer.Koukahatudou = false;
                    for (int t = 0; t < _directer.SearchImageList.Length; t++)
                    {
                        Destroy(_directer.SearchImageList[t].gameObject);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < _directer.SearchImageList.Length; i++)
            {
                if (_directer.SearchImageList[i].selected)
                {
                    Debug.Log(_directer.SearchImageList[i].gameObject.name);
                    switch (_directer.SearchImageList[i].gameObject.name)
                    {
                        case "Buns(Clone)":
                            Debug.Log("バンズサーチ");
                            search_id = 1;
                            _manager.CreateCard(search_id, hand.transform);
                            break;
                        case "Cheese(Clone)":
                            Debug.Log("チーズサーチ");
                            search_id = 5;
                            _manager.CreateCard(search_id, hand.transform);
                            break;
                        case "Egg(Clone)":
                            Debug.Log("エッグサーチ");
                            search_id = 7;
                            _manager.CreateCard(search_id, hand.transform);
                            break;
                        case "Lettuce(Clone)":
                            Debug.Log("レタスサーチ");
                            search_id = 6;
                            _manager.CreateCard(search_id, hand.transform);
                            break;
                        case "Muffin(Clone)":
                            Debug.Log("マフィンサーチ");
                            search_id = 3;
                            _manager.CreateCard(search_id, hand.transform);
                            break;
                        case "Patty(Clone)":
                            Debug.Log("パティサーチ");
                            search_id = 2;
                            _manager.CreateCard(search_id, hand.transform);
                            break;
                        case "Pickles(Clone)":
                            Debug.Log("ピクルスサーチ");
                            search_id = 4;
                            _manager.CreateCard(search_id, hand.transform);
                            break;
                        case "Tomato(Clone)":
                            Debug.Log("トマトサーチ");
                            search_id = 8;
                            _manager.CreateCard(search_id, hand.transform);
                            break;
                        case "Foodraw(Clone)":
                            Debug.Log("フードローサーチ");
                            search_id = 201;
                            _manager.CreateCard(search_id, hand.transform);
                            break;
                        case "Plan(Clone)":
                            Debug.Log("料理計画サーチ");
                            search_id = 202;
                            _manager.CreateCard(search_id, hand.transform);
                            break;
                        case "hasiyasume(Clone)":
                            Debug.Log("箸休めサーチ");
                            search_id = 203;
                            _manager.CreateCard(search_id, hand.transform);
                            break;
                    }
                    while (cnt == 0)
                    {
                        for (int n = 0; n < _manager.deck.Count; n++)
                        {
                            if (_manager.deck[n] == search_id)
                            {
                                _manager.deck.RemoveAt(n);
                                cnt++;
                                break;
                            }
                        }
                    }

                    cnt = 0;
                    panel_anim.SetTrigger("Down");
                    for (int t = 0; t < _directer.SearchImageList.Length; t++)
                    {
                        Destroy(_directer.SearchImageList[t].gameObject);
                    }
                }
            }
        }
        _directer.Koukahatudou = false;
    }
}
