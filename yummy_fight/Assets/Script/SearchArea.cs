using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour
{
    public GameObject[] image;
    [SerializeField]
    private GameObject scroll;
    public GameManager manage;
    public GameDirecter _directer;
    // Start is called before the first frame update
    void Start()
    {
        manage = GameObject.Find("GameManager").GetComponent<GameManager>();
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        //scroll.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatePrefab(int id)
    {
        Instantiate(image[id-1], this.transform);
    }

    public void CreatePrefab_plan(int id)
    {
        int cardID;
        if (manage.deck[id] > 200)
        {
            cardID = manage.deck[id] - 192;
        }
        else
        {
            cardID = manage.deck[id];
        }

        Instantiate(image[cardID -1 ], this.transform);
    }

    public void CreatePrefab_field(int id)
    {
        int cardID;
        switch(id)
        {
            case 104:
                for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
                {
                    cardID = _directer.playerFieldCardList[i].GetComponent<CardView>().cardID;
                    if (cardID > 100)
                    {
                        Instantiate(image[cardID - 90], this.transform);
                    }
                    else
                    {
                        Instantiate(image[cardID - 1], this.transform);
                    }
                }
                break;
            case 102:
                for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
                {
                    if(_directer.playerFieldCardList[i].GetComponent<CardController>().hirou)
                    {
                        cardID = _directer.playerFieldCardList[i].GetComponent<CardView>().cardID;
                        if (cardID > 100)
                        {
                            if (cardID != 102)
                            {
                                Instantiate(image[cardID - 90], this.transform);
                            }
                        }
                        else
                        {
                            Instantiate(image[cardID-1], this.transform);
                        }
                    }
                }
                break;
        }

    }
}
