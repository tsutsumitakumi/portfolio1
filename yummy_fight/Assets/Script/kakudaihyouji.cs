using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class kakudaihyouji : MonoBehaviour, IPointerClickHandler
{
    public string targetTag = "Card"; // 拡大表示するオブジェクトのタグ
    private RectTransform targetPanel; // 拡大表示するパネル
    private GameObject currentObjectInstance; // 現在表示されているオブジェクトのインスタンス

    void Start()
    {
        // kakudai パネルを検索して参照する
        GameObject kakudaiPanel = GameObject.Find("Kakudai");
        if (kakudaiPanel != null)
        {
            targetPanel = kakudaiPanel.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("kakudai パネルが見つかりませんでした。");
        }

        // シーンにEventSystemがない場合は追加する
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
    }

    // クリックされたときの処理
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerPress;
        if (clickedObject != null && clickedObject.CompareTag(targetTag))
        {
            // 拡大表示するオブジェクトがクリックされた場合
            if (currentObjectInstance == null)
            {
                Debug.Log("拡大");
                currentObjectInstance = Instantiate(clickedObject, targetPanel); // クリックされたオブジェクトのコピーを作成
                currentObjectInstance.transform.SetParent(targetPanel); // 拡大表示するパネルの子にする
                currentObjectInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // パネルの中央に配置
                currentObjectInstance.GetComponent<RectTransform>().localScale = Vector3.one * 2f; // 2倍のサイズに拡大
            }
            else
            {
                Debug.Log("削除");
                Destroy(currentObjectInstance); // すでに表示されている場合は削除
                currentObjectInstance = null;
            }
        }
        else
        {
            // 拡大表示するオブジェクト以外がクリックされた場合
            if (currentObjectInstance != null)
            {
                Debug.Log("削除２");
                Destroy(currentObjectInstance); 
                currentObjectInstance = null;
            }
        }
    }
}
