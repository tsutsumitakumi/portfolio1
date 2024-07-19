using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectHighlight : MonoBehaviour, IPointerClickHandler
{
    public GameObject Outline;
    public bool selected;
    public GameDirecter directer;

    private void Start()
    {
        directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        selected = false;
    }

    // �N���b�N���ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("�N���b�N����܂���");
        GameObject clickedObject = eventData.pointerPress;
        GameObject Outline = clickedObject.transform.Find("Outline").gameObject;
        Debug.Log(clickedObject + "���N���b�N����܂���");
        // �N���b�N���ꂽ�I�u�W�F�N�g��Player_field��ɂ���card�I�u�W�F�N�g�ł��邩�`�F�b�N
        if (transform.CompareTag("Card"))
        {
            //�A�E�g���C���̕\����Ԃ𔽓]������
            if(!selected)
            {
                if(directer.before_outline != null)
                {
                    directer.before_outline.SetActive(false);
                }

                if(directer.before_outline_object != null)
                {
                    directer.before_outline_object.GetComponent<ObjectHighlight>().selected = false;

                }
                Outline.SetActive(true);
                selected = true;

                directer.before_outline = Outline;
                directer.before_outline_object = clickedObject;
            }
            else
            {
                Outline.SetActive(false);
                selected = false;
            }
        }
        else
        {
            directer.before_outline.SetActive(false);
            directer.before_outline_object.GetComponent<ObjectHighlight>().selected = false;
        }
    }
}