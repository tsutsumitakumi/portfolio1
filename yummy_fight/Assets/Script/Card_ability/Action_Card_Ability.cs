using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Card_Ability : MonoBehaviour
{
    public GameManager manage;
    public CardMovement move_scr;
    public GameDirecter directer;
    public bool Use_Avility;

    public SearchArea search_script;
    public GameObject scroll_view;
    public Transform hand;
    public CardController[] Search_Card;
    public Animator panel_anim, kouka;
    SE_Controller SE;

    private int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        SE = GameObject.Find("SE").GetComponent<SE_Controller>();
        search_script = GameObject.Find("Content").GetComponent<SearchArea>();
        manage = GameObject.Find("GameManager").GetComponent<GameManager>();
        directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        scroll_view = GameObject.Find("Search_Area");
        hand = GameObject.Find("Player_hand").transform;
        move_scr = this.gameObject.GetComponent<CardMovement>();
        panel_anim = scroll_view.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move_scr.cardParent != null)
        {
            if (!this.Use_Avility && move_scr.cardParent == GameObject.Find("Action_Space").transform)
            {
                switch (this.GetComponent<CardView>().cardID)
                {
                    case 201:
                        if (!manage.Foodraw)
                        {
                            SE.Ability_SE();
                            kouka.SetTrigger("Kouka");
                            StartCoroutine("Foodraw");
                        }
                        break;
                    case 202:
                        if(!manage.Plan)
                        {
                            SE.Ability_SE();
                            kouka.SetTrigger("Kouka");
                            StartCoroutine("Plan");
                            Plan();
                        }
                        break;
                    case 203:
                        if(!manage.Stop)
                        {
                            SE.Ability_SE();
                            kouka.SetTrigger("Kouka");
                            manage.Stop = true;
                            directer.enemyattack = false;
                        }
                        break;
                }
            }
        }
    }
    public IEnumerator Foodraw()
    {
        Use_Avility = true;
        manage.Foodraw = true;
        yield return new WaitForSeconds(1);
        manage.DrawCard(hand.transform);
        manage.DrawCard(hand.transform);
        this.gameObject.GetComponent<CardController>().Destroy_me();

    }

    public IEnumerator Plan()
    {
        Use_Avility = true;
        manage.Plan = true;
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<CardController>().Destroy_me();
        for (int i = 0; i < directer.SearchImageList.Length; i++)
        {
            Destroy(directer.SearchImageList[i].gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            search_script.CreatePrefab_plan(i);
            cnt++;
        }

        if (cnt != 0)
        {
            panel_anim.SetTrigger("Up");
            scroll_view.SetActive(true);
            cnt = 0;
        }


    }
}
