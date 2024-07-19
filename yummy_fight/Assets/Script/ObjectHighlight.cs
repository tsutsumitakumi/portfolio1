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

    // クリックされたときに呼び出されるメソッド
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("クリックされました");
        GameObject clickedObject = eventData.pointerPress;
        GameObject Outline = clickedObject.transform.Find("Outline").gameObject;
        Debug.Log(clickedObject + "がクリックされました");
        // クリックされたオブジェクトがPlayer_field上にあるcardオブジェクトであるかチェック
        if (transform.CompareTag("Card"))
        {
            //アウトラインの表示状態を反転させる
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