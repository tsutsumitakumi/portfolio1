using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class yugouhantei : MonoBehaviour
{
    public GameManager manage_script;
    public GameDirecter directer;
    public GameObject popup;
    public Transform playerField;


    public int yugou;

    public bool harf;       //  �n�[�t�o�[�K�[�����邩�̌��m
    //����Ȃ��f�ނ����ׂĎg���̂�j�~�����I�I
    bool bans;              
    bool pati;                   



    public CardController[] playerFieldCardList;//�t�B�[���h�̃J�[�h���i�[���郊�X�g

    public Bagamu bagam;
    public chibagrw chiba;
    public haahhhbaer har;
    // Start is called before the first frame update
    void Start()
    {
        manage_script = GameObject.Find("GameManager").GetComponent<GameManager>();
        directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();

        //bagam = GameObject.Find("bagamute").GetComponent<Bagamu>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Yugousyoukan();
        //OnClick();

    }
    public void OnClick()
    {
        if(directer.playerFieldCardList.Length < 3)  //3���ȏ�͏o���Ȃ��悤�ɂ���
        {
            switch (yugou)
            {
                case 101:   //�o�K���[�g�̏���
                    manage_script.CreateCard(101, playerField);
                    for (int i = 0; i < directer.playerkitchenCardList.Length; i++)
                    {
                        if (harf == true)
                        {
                            if (directer.playerkitchenCardList[i].gameObject.GetComponent<CardView>().cardID == 105)
                            {
                                Destroy(directer.playerkitchenCardList[i].gameObject);
                                harfbagamu();
                                harf = false;
                            }
                        }
                        else if (directer.playerkitchenCardList[i].gameObject.GetComponent<CardView>().cardID == 1 || directer.playerkitchenCardList[i].gameObject.GetComponent<CardView>().cardID == 3 && bans == false)
                        {
                            Destroy(directer.playerkitchenCardList[i].gameObject);
                            for (int a = 0; a < directer.playerkitchenCardList.Length; a++)
                            {
                                if (directer.playerkitchenCardList[a].gameObject.GetComponent<CardView>().cardID == 2 && pati == false)
                                {
                                    Destroy(directer.playerkitchenCardList[a].gameObject);
                                    harfbagamu();
                                    pati = true;
                                }
                            }
                        }
                    }
                    bans = false;
                    pati = false;
                    break;
                case 102:    //�G�O�}�t�̏���
                    manage_script.CreateCard(102, playerField);
                    for (int i = 0; i < directer.playerkitchenCardList.Length; i++)
                    {
                        if (harf == true)
                        {
                            if (directer.playerkitchenCardList[i].gameObject.GetComponent<CardView>().cardID == 105)
                            {
                                Destroy(directer.playerkitchenCardList[i].gameObject);
                                egtu();
                                harf = false;
                                break;
                            }
                        }
                        else if (directer.playerkitchenCardList[i].gameObject.GetComponent<CardView>().cardID == 2 && pati == false)
                        {
                            Destroy(directer.playerkitchenCardList[i].gameObject);
                            pati = true;
                            for (int a = 0; a < directer.playerkitchenCardList.Length; a++)
                            {
                                if (directer.playerkitchenCardList[a].gameObject.GetComponent<CardView>().cardID == 3 && bans == false)
                                {
                                    Destroy(directer.playerkitchenCardList[a].gameObject);
                                    bans = true;
                                    egtu();
                                    break;
                                }
                            }
                        }
                    }
                    pati = false;
                    bans = false;
                    break;
                case 103:       //�g���o�K�̏���
                    manage_script.CreateCard(103, playerField);
                    for (int i = 0; i < directer.playerkitchenCardList.Length; i++)
                    {
                        if (directer.playerkitchenCardList[i].gameObject.GetComponent<CardView>().cardID == 8)
                        {
                            Destroy(directer.playerkitchenCardList[i].gameObject);
                            for (int a = 0; a < directer.playerkitchenCardList.Length; a++)
                            {
                                if (directer.playerkitchenCardList[a].gameObject.GetComponent<CardView>().cardID == 6)
                                {
                                    Destroy(directer.playerkitchenCardList[a].gameObject);
                                    for (int j = 0; j < directer.playerkitchenCardList.Length; j++)
                                    {
                                        if (directer.playerkitchenCardList[j].gameObject.GetComponent<CardView>().cardID == 3 || directer.playerkitchenCardList[j].gameObject.GetComponent<CardView>().cardID == 1 && bans == false)
                                        {
                                            Destroy(directer.playerkitchenCardList[j].gameObject);
                                            popup.SetActive(false);
                                            yugou = 0;
                                            bans = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    bans = false;
                    break;
                case 104:       //�`�[�o�K�̏���
                    manage_script.CreateCard(104, playerField);
                    for (int i = 0; i < directer.playerkitchenCardList.Length; i++)
                    {
                        if (directer.playerkitchenCardList[i].gameObject.GetComponent<CardView>().cardID == 5)
                        {
                            Destroy(directer.playerkitchenCardList[i].gameObject);
                            for (int a = 0; a < directer.playerkitchenCardList.Length; a++)
                            {
                                if (harf == true)
                                {
                                    if (directer.playerkitchenCardList[a].gameObject.GetComponent<CardView>().cardID == 105)
                                    {
                                        Destroy(directer.playerkitchenCardList[a].gameObject);
                                        popup.SetActive(false);
                                        yugou = 0;
                                        harf = false;
                                        break;
                                    }
                                }
                                if (directer.playerkitchenCardList[a].gameObject.GetComponent<CardView>().cardID == 2 && pati == false)
                                {
                                    Destroy(directer.playerkitchenCardList[a].gameObject);
                                    for (int j = 0; j < directer.playerkitchenCardList.Length; j++)
                                    {
                                        if ((directer.playerkitchenCardList[j].gameObject.GetComponent<CardView>().cardID == 1 || directer.playerkitchenCardList[j].gameObject.GetComponent<CardView>().cardID == 3) && bans == false)
                                        {
                                            Destroy(directer.playerkitchenCardList[j].gameObject);
                                            popup.SetActive(false);
                                            yugou = 0;
                                            bans = true;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    bans = false;
                    pati = false;
                    break;
                case 105:       //�n�[�t�o�[�K�[�̏���
                    manage_script.CreateCard(105, playerField);
                    for (int i = 0; i < directer.playerkitchenCardList.Length; i++)
                    {
                        if (directer.playerkitchenCardList[i].gameObject.GetComponent<CardView>().cardID == 2 && pati == false)
                        {
                            Destroy(directer.playerkitchenCardList[i].gameObject);
                            Debug.Log("�f�ލ폜");
                            pati = true;
                            for (int a = 0; a < directer.playerkitchenCardList.Length; a++)
                            {
                                if (directer.playerkitchenCardList[a].gameObject.GetComponent<CardView>().cardID == 1 || directer.playerkitchenCardList[a].gameObject.GetComponent<CardView>().cardID == 3 && bans == false)
                                {
                                    Destroy(directer.playerkitchenCardList[a].gameObject);
                                    Debug.Log("�f�ލ폜2");
                                    popup.SetActive(false);
                                    yugou = 0;
                                    bans = true;
                                    break;
                                }
                            }
                        }
                    }
                    bans = false;
                    pati = false;
                    break;

            }
        }

    }

    public void harfbagamu()
    {
        for (int w = 0; w < directer.playerkitchenCardList.Length; w++)
        {
            if (directer.playerkitchenCardList[w].gameObject.GetComponent<CardView>().cardID == 4)
            {
                Destroy(directer.playerkitchenCardList[w].gameObject);
                for (int y = 0; y < directer.playerkitchenCardList.Length; y++)
                {
                    if (directer.playerkitchenCardList[y].gameObject.GetComponent<CardView>().cardID == 6)
                    {
                        Destroy(directer.playerkitchenCardList[y].gameObject);
                        for (int m = 0; m < directer.playerkitchenCardList.Length; m++)
                        {
                            if (directer.playerkitchenCardList[m].gameObject.GetComponent<CardView>().cardID == 8)
                            {
                                Destroy(directer.playerkitchenCardList[m].gameObject);
                                popup.SetActive(false);
                                yugou = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public void egtu()
    {
        for (int j = 0; j < directer.playerkitchenCardList.Length; j++)
        {
            if (directer.playerkitchenCardList[j].gameObject.GetComponent<CardView>().cardID == 7)
            {
                Destroy(directer.playerkitchenCardList[j].gameObject);
                popup.SetActive(false);
                yugou = 0;
                break;
            }
        }
    }
}
