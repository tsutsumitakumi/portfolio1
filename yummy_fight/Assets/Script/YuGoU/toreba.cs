using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class toreba : MonoBehaviour, IPointerClickHandler
{

    public CardController[] playerkitchenCardList;//調理場のカードを格納するリスト

    public GameManager manage_script;
    public GameDirecter directer;
    public GameObject popup;
    public Transform playerField;
    public yugouhantei yug,harf;

    bool click = true;
    bool aiue;
    // Start is called before the first frame update
    void Start()
    {
        manage_script = GameObject.Find("GameManager").GetComponent<GameManager>();
        directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        harf = GameObject.Find("popup/YugouButton").GetComponent<yugouhantei>();
        //yug = GameObject.Find("YugouButton").GetComponent<yugouhantei>();
        //popup = GameObject.Find("popup").GetComponent<GameObject>();
        popup.SetActive(false);
        Debug.Log("色が変わります");
        gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        Debug.Log("色が変わりました");

    }

    void Update()
    {
        if (directer.playerkitchenCardList[0] != null)
        {
            gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            for (int i = 0; i < directer.playerkitchenCardList.Length; i++)
            {
                if (directer.playerkitchenCardList[i].gameObject.GetComponent<CardView>().cardID == 8)
                {
                    for (int a = 0; a < directer.playerkitchenCardList.Length; a++)
                    {
                        if (directer.playerkitchenCardList[a].gameObject.GetComponent<CardView>().cardID == 6)
                        {
                            for (int j = 0; j < directer.playerkitchenCardList.Length; j++)
                            {
                                    if (directer.playerkitchenCardList[j].gameObject.GetComponent<CardView>().cardID == 105)
                                    {
                                        if(directer.phase == GameDirecter.Phase.MAIN)
                                        {
                                            harf.harf = true;
                                            gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                                            aiue = true;
                                        }
                                        else
                                        {
                                        popup.SetActive(false);
                                        aiue = false;
                                        }

                                    }
                                        else if (directer.playerkitchenCardList[j].gameObject.GetComponent<CardView>().cardID == 3 || directer.playerkitchenCardList[j].gameObject.GetComponent<CardView>().cardID == 1)
                                        {
                                        if(directer.phase == GameDirecter.Phase.MAIN)
                                        {
                                            gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                                            aiue = true;
                                        }

                                    }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            aiue = false;
        }

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (click)
        {

            if (aiue == true)
            {
                popup.SetActive(true);
                yug.yugou = 103;
            }

        }
    }
}