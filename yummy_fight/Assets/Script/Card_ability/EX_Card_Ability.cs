using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_Card_Ability : MonoBehaviour
{
    public GameDirecter directer_script;
    public GameObject select_area;
    public SearchArea search_script;
    public GameManager _manage;
    public GameObject scroll_view;
    public Animator panel_anim, kouka;
    SE_Controller SE;

    public CardController[] kaihuku_card;//敵のフィールドのカードを格納するリスト
    // Start is called before the first frame update
    void Start()
    {
        search_script = GameObject.Find("Content").GetComponent<SearchArea>();
        SE = GameObject.Find("SE").GetComponent<SE_Controller>();
        directer_script = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        _manage = GameObject.Find("GameManager").GetComponent<GameManager>();
        scroll_view = GameObject.Find("Search_Area");
        panel_anim = scroll_view.GetComponent<Animator>();
        switch (this.GetComponent<CardView>().cardID)
        {
            case 101:
                Debug.Log("バガムート");
                directer_script.Ensyutu_Start();
                StartCoroutine("Bagamute");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Bagamute()
    {
        _manage.bagamute = true;
        SE.Ability_SE();
        yield return new WaitForSeconds(4);
        directer_script.Ensyutu_End();
        kouka.SetTrigger("Kouka");
        Debug.Log(directer_script.EnemyFieldCardList.Length);
        if (!directer_script.E_turn)
        {
            for (int i = 0; i < directer_script.EnemyFieldCardList.Length; i++)
            {
                Debug.Log(directer_script.EnemyFieldCardList[i].gameObject);

                directer_script.EnemyFieldCardList[i].gameObject.GetComponent<CardController>().Destroy_me();
            }

            for (int t = 0; t < directer_script.playerFieldCardList.Length; t++)
            {
                Debug.Log(directer_script.playerFieldCardList[t].gameObject);
                if (directer_script.playerFieldCardList[t].gameObject.GetComponent<CardView>().cardID != 101)
                {
                    directer_script.playerFieldCardList[t].gameObject.GetComponent<CardController>().Destroy_me();
                }
            }
        }
        else
        {
            for (int i = 0; i < directer_script.playerFieldCardList.Length; i++)
            {
                directer_script.playerFieldCardList[i].gameObject.GetComponent<CardController>().Destroy_me();
            }

            for (int t = 0; t < directer_script.EnemyFieldCardList.Length; t++)
            {
                if (directer_script.EnemyFieldCardList[t].gameObject.GetComponent<CardView>().cardID != 101)
                {
                    directer_script.EnemyFieldCardList[t].gameObject.GetComponent<CardController>().Destroy_me();
                }
            }
        }

        
    }

    public IEnumerator Egumahu()

    {
        SE.Ability_SE();
        yield return new WaitForSeconds(1);
        kouka.SetTrigger("Kouka");
        _manage.egumahu = true;
        SelectCard();
        this.gameObject.GetComponent<CardController>().Hirou();
        this.gameObject.GetComponent<CardController>().egumahu_aru = false ;
    }

    public IEnumerator Torebaga()
    {
        SE.Ability_SE();
        yield return new WaitForSeconds(1);
        _manage.torabaga = true;
        //ブロック時回復
        this.gameObject.GetComponent<CardController>().kaihuku();
        
        kouka.SetTrigger("Kouka");
    }

    public IEnumerator Chibaga()
    {
        SE.Ability_SE();
        yield return new WaitForSeconds(1);
        kouka.SetTrigger("Kouka");
        _manage.chibaga = true;
        SelectCard();
    }

    public void SelectCard()
    {
        for (int i = 0; i < directer_script.SearchImageList.Length; i++)
        {
            Destroy(directer_script.SearchImageList[i].gameObject);
        }

        search_script.CreatePrefab_field(this.GetComponent<CardView>().cardID);
        panel_anim.SetTrigger("Up");
        scroll_view.SetActive(true);
    }

}
