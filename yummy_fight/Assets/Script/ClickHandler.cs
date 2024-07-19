using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    private RectTransform targetPanel; // 拡大表示する画像が表示されるパネル
    private GameObject currentCard; // 現在表示されているカードのインスタンス
    private GameObject kakudaiPanel;
    private const float ScaleFactor = 3f; // 画像の拡大倍率

    void Start()
    {
        // Kakudai パネルを検索して参照する
        kakudaiPanel = GameObject.Find("Kakudai");
        if (kakudaiPanel != null)
        {
            targetPanel = kakudaiPanel.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("Kakudai Panel not found!");
        }

        // シーンにEventSystemがない場合は追加する
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
    }

    // クリックされたときに呼ばれる関数
    public void OnPointerClick(PointerEventData eventData)
    {
        // クリックされた場所にオブジェクトがあるかチェック
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            Debug.Log(eventData.pointerCurrentRaycast.gameObject);
            // クリックされたオブジェクトがCardタグを持っているかチェック
            if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Card"))
            {
                // クリックされたオブジェクトに割り当てられているImageコンポーネントを取得
                Image cardImage = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>();
                if (cardImage != null)
                {
                    Debug.Log("正常");
                    // 既存のCardPreviewが存在する場合は削除する
                    DestroyCurrentCardPreview();

                    kakudaiPanel.GetComponent<Image>().sprite = cardImage.sprite;
                    kakudaiPanel.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                }
                Debug.Log("処理終了");
            }
            else
            {
                Debug.Log("タグなし");
                // kakudaiタグのついたオブジェクトを破棄
                DestroyCurrentCardPreview();
            }
        }
        else
        {
            Debug.Log("オブジェクトなし");
            // kakudaiタグのついたオブジェクトを破棄
            DestroyCurrentCardPreview();
        }
    }

    // 現在のカードプレビューを削除するメソッド
    private void DestroyCurrentCardPreview()
    {
        kakudaiPanel.GetComponent<Image>().color = new Color(255,255,255,0);

        currentCard = null;
    }
}
