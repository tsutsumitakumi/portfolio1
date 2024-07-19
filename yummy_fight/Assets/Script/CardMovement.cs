using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform cardParent;
    private Vector2 prevPos;
    public Transform before_parent;
    public GameManager manage_script;
    public bool kitchen, field, change, action;
    public bool Ekitchen, Efield, Echange, Eaction;
    GameDirecter directer_script;

    public bool select;

    void Start()
    {
        manage_script = GameObject.Find("GameManager").GetComponent<GameManager>();
        directer_script = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        cardParent = transform.parent.gameObject.transform;
        change = true;
        Debug.Log(this.transform.localScale);
        transform.eulerAngles = new Vector3(0, 0, 0); // X軸を中心に45°回転、Y軸Z軸は初期値
        select = false;

        if(cardParent.gameObject.CompareTag("Enemy"))
        {
            transform.rotation = Quaternion.Euler(180,0,0);
        }

    }

    void Update()
    {

        if (directer_script.playerkitchenCardList.Length  < 5)//調理場のカードが５枚未満の時に置けるようにする
        {
            kitchen = true;
        }
        else
        {
            kitchen = false;
        }

        if (directer_script.EnemyKitchenCardList.Length < 5)//調理場のカードが５枚未満の時に置けるようにする ...敵
        {
            Ekitchen = true;
        }
        else
        {
            Ekitchen = false;
        }

        if (directer_script.playerFieldCardList.Length < 3 && cardParent == GameObject.Find("Player_kitchen").transform)//フィールドのカードが３枚未満の時に置けるようにする
        {
            field = true;
        }
        else
        {
            field = false;
        }

        if (directer_script.EnemyFieldCardList.Length < 3 && cardParent == GameObject.Find("Enemy_kitchen").transform)//フィールドのカードが３枚未満の時に置けるようにする
        {
            Efield = true;
        }
        else
        {
            Efield = false;
        }

        if (directer_script.Movable && change)
        {
            Debug.Log("動けます");
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
            change = false;
        }

        if(!directer_script.Movable)
        {
            Debug.Log("動けません");
            change = true;
            //GetComponent<CanvasGroup>().blocksRaycasts = false; // blocksRaycastsをオフにする
        }

        if(this.gameObject.GetComponent<CardView>().cardID > 200)
        {
            action = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData) // ドラッグを始めるときに行う処理
    {
        if(!cardParent.gameObject.CompareTag("Enemy"))
        {
            before_parent = transform.parent.gameObject.transform;
            prevPos = this.transform.position;
            cardParent = transform.parent;
            transform.SetParent(cardParent.parent);
            GetComponent<CanvasGroup>().blocksRaycasts = false; // blocksRaycastsをオフにする
        }
    }

    public void OnDrag(PointerEventData eventData) // ドラッグした時に起こす処理
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) // カードを離したときに行う処理
    {
        change = true;
        //スタンバイフェーズの移動(フィールドからキッチン)
        if (kitchen && cardParent == GameObject.Find("Player_kitchen").transform && before_parent == GameObject.Find("Player_field").transform && !directer_script.Summonable && !directer_script.Attackable && !action)//調理場にカードを置く処理
        {
            transform.SetParent(cardParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
        }
        //スタンバイフェーズの移動(キッチンからフィールド)
        else if (field && cardParent == GameObject.Find("Player_field").transform && before_parent == GameObject.Find("Player_kitchen").transform && !directer_script.Summonable && !directer_script.Attackable && !action)//フィールドにカードを置く処理
        {
            transform.SetParent(cardParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
        }
        //メインフェーズの召喚(手札からキッチン)
        else if (kitchen && cardParent == GameObject.Find("Player_kitchen").transform && before_parent == GameObject.Find("Player_hand").transform && directer_script.Summonable && !directer_script.Attackable && !action)
        {
            transform.SetParent(cardParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
        }
        else if (action && cardParent == GameObject.Find("Action_Space").transform && before_parent == GameObject.Find("Player_hand").transform && directer_script.Summonable)
        {
            if(this.gameObject.GetComponent<CardView>().cardID == 203)
            {
                this.transform.position = prevPos;
                cardParent = before_parent;
                transform.SetParent(before_parent);
                GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
            }
            else
            {
                transform.SetParent(cardParent);
            }
        }
        else if(this.gameObject.GetComponent<CardView>().cardID == 203 && directer_script.Zekkouhyoujun)
        {
            transform.SetParent(cardParent);
            StartCoroutine("Destroy");
        }

        else
        {
            this.transform.position = prevPos;
            cardParent = before_parent;
            transform.SetParent(before_parent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
        };
    }

    public void EOnEndDrag() // カードを離したときに行う処理
    {
        change = true;
        //スタンバイフェーズの移動(フィールドからキッチン)
        if (kitchen && cardParent == GameObject.Find("Enemy_kitchen").transform && before_parent == GameObject.Find("Enemy_field").transform && !directer_script.Summonable && !directer_script.Attackable && !action)//調理場にカードを置く処理
        {
            transform.SetParent(cardParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
        }
        //スタンバイフェーズの移動(キッチンからフィールド)
        else if (field && cardParent == GameObject.Find("Enemy_field").transform && before_parent == GameObject.Find("Enemy_kitchen").transform && !directer_script.Summonable && !directer_script.Attackable && !action)//フィールドにカードを置く処理
        {
            transform.SetParent(cardParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
        }
        //メインフェーズの召喚(手札からキッチン)
        else if (kitchen && cardParent == GameObject.Find("Enemy_kitchen").transform && before_parent == GameObject.Find("Enemy_hand").transform && directer_script.Summonable && !directer_script.Attackable && !action)
        {
            transform.SetParent(cardParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
        }
        else if (action && cardParent == GameObject.Find("Action_Space").transform && before_parent == GameObject.Find("Enemy_hand").transform && directer_script.Summonable)
        {
            if (this.gameObject.GetComponent<CardView>().cardID == 203)
            {
                this.transform.position = prevPos;
                cardParent = before_parent;
                transform.SetParent(before_parent);
                GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
            }
            else
            {
                transform.SetParent(cardParent);
            }
        }
        else if (this.gameObject.GetComponent<CardView>().cardID == 203 && directer_script.Zekkouhyoujun)
        {
            transform.SetParent(cardParent);
            StartCoroutine("Destroy");
        }

        else
        {
            this.transform.position = prevPos;
            cardParent = before_parent;
            transform.SetParent(before_parent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
        };
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
