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
        transform.eulerAngles = new Vector3(0, 0, 0); // X���𒆐S��45����]�AY��Z���͏����l
        select = false;

        if(cardParent.gameObject.CompareTag("Enemy"))
        {
            transform.rotation = Quaternion.Euler(180,0,0);
        }

    }

    void Update()
    {

        if (directer_script.playerkitchenCardList.Length  < 5)//������̃J�[�h���T�������̎��ɒu����悤�ɂ���
        {
            kitchen = true;
        }
        else
        {
            kitchen = false;
        }

        if (directer_script.EnemyKitchenCardList.Length < 5)//������̃J�[�h���T�������̎��ɒu����悤�ɂ��� ...�G
        {
            Ekitchen = true;
        }
        else
        {
            Ekitchen = false;
        }

        if (directer_script.playerFieldCardList.Length < 3 && cardParent == GameObject.Find("Player_kitchen").transform)//�t�B�[���h�̃J�[�h���R�������̎��ɒu����悤�ɂ���
        {
            field = true;
        }
        else
        {
            field = false;
        }

        if (directer_script.EnemyFieldCardList.Length < 3 && cardParent == GameObject.Find("Enemy_kitchen").transform)//�t�B�[���h�̃J�[�h���R�������̎��ɒu����悤�ɂ���
        {
            Efield = true;
        }
        else
        {
            Efield = false;
        }

        if (directer_script.Movable && change)
        {
            Debug.Log("�����܂�");
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycasts���I���ɂ���
            change = false;
        }

        if(!directer_script.Movable)
        {
            Debug.Log("�����܂���");
            change = true;
            //GetComponent<CanvasGroup>().blocksRaycasts = false; // blocksRaycasts���I�t�ɂ���
        }

        if(this.gameObject.GetComponent<CardView>().cardID > 200)
        {
            action = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData) // �h���b�O���n�߂�Ƃ��ɍs������
    {
        if(!cardParent.gameObject.CompareTag("Enemy"))
        {
            before_parent = transform.parent.gameObject.transform;
            prevPos = this.transform.position;
            cardParent = transform.parent;
            transform.SetParent(cardParent.parent);
            GetComponent<CanvasGroup>().blocksRaycasts = false; // blocksRaycasts���I�t�ɂ���
        }
    }

    public void OnDrag(PointerEventData eventData) // �h���b�O�������ɋN��������
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) // �J�[�h�𗣂����Ƃ��ɍs������
    {
        change = true;
        //�X�^���o�C�t�F�[�Y�̈ړ�(�t�B�[���h����L�b�`��)
        if (kitchen && cardParent == GameObject.Find("Player_kitchen").transform && before_parent == GameObject.Find("Player_field").transform && !directer_script.Summonable && !directer_script.Attackable && !action)//������ɃJ�[�h��u������
        {
            transform.SetParent(cardParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycasts���I���ɂ���
        }
        //�X�^���o�C�t�F�[�Y�̈ړ�(�L�b�`������t�B�[���h)
        else if (field && cardParent == GameObject.Find("Player_field").transform && before_parent == GameObject.Find("Player_kitchen").transform && !directer_script.Summonable && !directer_script.Attackable && !action)//�t�B�[���h�ɃJ�[�h��u������
        {
            transform.SetParent(cardParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycasts���I���ɂ���
        }
        //���C���t�F�[�Y�̏���(��D����L�b�`��)
        else if (kitchen && cardParent == GameObject.Find("Player_kitchen").transform && before_parent == GameObject.Find("Player_hand").transform && directer_script.Summonable && !directer_script.Attackable && !action)
        {
            transform.SetParent(cardParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycasts���I���ɂ���
        }
        else if (action && cardParent == GameObject.Find("Action_Space").transform && before_parent == GameObject.Find("Player_hand").transform && directer_script.Summonable)
        {
            if(this.gameObject.GetComponent<CardView>().cardID == 203)
            {
                this.transform.position = prevPos;
                cardParent = before_parent;
                transform.SetParent(before_parent);
                GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycasts���I���ɂ���
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
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycasts���I���ɂ���
        };
    }

    public void EOnEndDrag() // �J�[�h�𗣂����Ƃ��ɍs������
    {
        change = true;
        //�X�^���o�C�t�F�[�Y�̈ړ�(�t�B�[���h����L�b�`��)
        if (kitchen && cardParent == GameObject.Find("Enemy_kitchen").transform && before_parent == GameObject.Find("Enemy_field").transform && !directer_script.Summonable && !directer_script.Attackable && !action)//������ɃJ�[�h��u������
        {
            transform.SetParent(cardParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycasts���I���ɂ���
        }
        //�X�^���o�C�t�F�[�Y�̈ړ�(�L�b�`������t�B�[���h)
        else if (field && cardParent == GameObject.Find("Enemy_field").transform && before_parent == GameObject.Find("Enemy_kitchen").transform && !directer_script.Summonable && !directer_script.Attackable && !action)//�t�B�[���h�ɃJ�[�h��u������
        {
            transform.SetParent(cardParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycasts���I���ɂ���
        }
        //���C���t�F�[�Y�̏���(��D����L�b�`��)
        else if (kitchen && cardParent == GameObject.Find("Enemy_kitchen").transform && before_parent == GameObject.Find("Enemy_hand").transform && directer_script.Summonable && !directer_script.Attackable && !action)
        {
            transform.SetParent(cardParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycasts���I���ɂ���
        }
        else if (action && cardParent == GameObject.Find("Action_Space").transform && before_parent == GameObject.Find("Enemy_hand").transform && directer_script.Summonable)
        {
            if (this.gameObject.GetComponent<CardView>().cardID == 203)
            {
                this.transform.position = prevPos;
                cardParent = before_parent;
                transform.SetParent(before_parent);
                GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycasts���I���ɂ���
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
            GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycasts���I���ɂ���
        };
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
