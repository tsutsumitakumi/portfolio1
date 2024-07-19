using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Ability : MonoBehaviour
{
    public GameManager manage;
    public CardMovement move_scr;
    public GameDirecter directer;
    public bool Use_Avility;

    public SearchArea search_script;
    public GameObject scroll_view;
    public CardController[] Search_Card;
    public Animator panel_anim,kouka;
    SE_Controller SE;

    private int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        search_script = GameObject.Find("Content").GetComponent<SearchArea>();
        SE = GameObject.Find("SE").GetComponent<SE_Controller>();
        manage = GameObject.Find("GameManager").GetComponent<GameManager>();
        directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        scroll_view = GameObject.Find("Search_Area");
        move_scr = this.gameObject.GetComponent<CardMovement>();
        panel_anim = scroll_view.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(move_scr.cardParent != null)//親要素がヌルじゃなければ
        {
            if (!this.Use_Avility && move_scr.cardParent == GameObject.Find("Player_kitchen").transform)
            {
                switch (this.GetComponent<CardView>().cardID)
                {
                    case 1:
                        if(!manage.Buns)
                        {
                            SE.Ability_SE();
                            kouka.SetTrigger("Kouka");
                            StartCoroutine("Buns");
                        }
                        else
                        {
                            this.Use_Avility = true;
                        }
                        break;
                    case 2:
                        if (!manage.Patty)
                        {
                            SE.Ability_SE();
                            kouka.SetTrigger("Kouka");
                            StartCoroutine("Patty");
                        }
                        else
                        {
                            this.Use_Avility = true;
                        }
                        break;
                    case 3:
                        if (!manage.Muffin)
                        {
                            SE.Ability_SE();
                            kouka.SetTrigger("Kouka");
                            StartCoroutine("Muffin");
                        }
                        else
                        {
                            this.Use_Avility = true;
                        }
                        break;
                    case 4:
                        if(!manage.Pickles)
                        {
                            SE.Ability_SE();
                            kouka.SetTrigger("Kouka");
                            StartCoroutine("Pickles");
                        }
                        else
                        {
                            this.Use_Avility = true;
                        }
                        break;
                }
            }
        }

    }

    public IEnumerator Buns()
    {
        Use_Avility = true;
        manage.Buns = true;
        yield return new WaitForSeconds(1);
        SearchCard(manage.playerHand, 2);//パティサーチ

    }
    public IEnumerator Patty()
    {
        Use_Avility = true;
        manage.Patty = true;
        yield return new WaitForSeconds(1);
        SearchCard(manage.playerHand, 1);//バンズをサーチ
        SearchCard(manage.playerHand, 3);//マフィンをサーチ

    }

    public IEnumerator Muffin()
    {
        Use_Avility = true;
        manage.Muffin = true;
        yield return new WaitForSeconds(1);
        SearchCard(manage.playerHand, 2);//パティサーチ

    }

    public IEnumerator Pickles()
    {
        Use_Avility = true;
        manage.Pickles = true;
        yield return new WaitForSeconds(1);
        SearchCard(manage.playerHand, 5);
        SearchCard(manage.playerHand, 6);
        SearchCard(manage.playerHand, 7);
        SearchCard(manage.playerHand, 8);

    }

    public void Foodraw()
    {
        manage.DrawCard(manage.playerField);
        manage.DrawCard(manage.playerField);
    }




    public void SearchCard(Transform hand, int Cardid)
    {
       for(int i = 0;i<directer.SearchImageList.Length;i++)
        {
            Destroy(directer.SearchImageList[i].gameObject);
        }

        for (int i = 0;i<manage.deck.Count;i++)
        {
            if(manage.deck[i] == Cardid)
            {
                search_script.CreatePrefab(Cardid);
                cnt++;
            }
        }

        if(cnt != 0)
        {
            panel_anim.SetTrigger("Up");
            scroll_view.SetActive(true);
            cnt = 0;
        }
    }
}
