using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    public GameObject attackButton;
    public bool cardSelected = false;

    void Start()
    {
        // 最初は攻撃ボタンを非表示にする
        attackButton.SetActive(false);
    }

    void Update()
    {
        // マウスの左クリックが押されたかどうかを確認し、カードがクリックされた場合にフラグを設定する
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Raycastの可視化用に、Rayの始点と終点を計算
            Vector2 origin = transform.position; // カードの位置を始点とする
            Vector2 direction = mousePosition - origin; // カードからマウス位置への方向を終点とする

            // Raycastを実行
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Card"))
                {
                    cardSelected = true;
                    Debug.Log("Card Clicked: " + hit.collider.gameObject.name); // デバッグログでクリックされたカードの名前を表示
                    OnCardHit(); // カードがクリックされた時の追加の処理を実行
                    break;
                }
            }

            // Rayの可視化
            Debug.DrawLine(origin, origin + direction, Color.red, 0.1f);
        }

        // カードが選択されているかどうかを確認し、攻撃ボタンの表示状態を更新する
        attackButton.SetActive(cardSelected);
    }

    // カードがクリックされた時の処理
    void OnCardHit()
    {
        // カードがクリックされた時の追加の処理をここに記述する
    }
}
