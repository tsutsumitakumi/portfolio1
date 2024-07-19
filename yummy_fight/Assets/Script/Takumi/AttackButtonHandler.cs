using UnityEngine;
using UnityEngine.UI;

public class AttackButtonHandler : MonoBehaviour
{
    public GameObject attackButtonObject; // AttackButtonのGameObject
    public GameObject blockButton; // ブロックボタン
    public GameObject lifeButton; // ライフで受けるボタン

    void Start()
    {
        // 初期状態ではブロックボタンとライフボタンを非表示にする
        blockButton.SetActive(false);
        lifeButton.SetActive(false);
    }

    public void OnAttackButtonPressed()
    {
        // 攻撃ボタンが押された時の処理

        // AttackButtonがアクティブである場合のみ、ブロックボタンとライフボタンを表示する
        if (attackButtonObject.activeSelf)
        {
            blockButton.SetActive(true);
            lifeButton.SetActive(true);
        }
    }
}
