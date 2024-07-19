using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Winclick : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        // TextMeshProコンポーネントを取得
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // 初期状態では非表示にする
        textMeshPro.gameObject.SetActive(false);

        // 4秒後に表示する
        Invoke("ShowTextMeshPro", 3.5f);
    }

    void ShowTextMeshPro()
    {
        // TextMeshProを表示する
        textMeshPro.gameObject.SetActive(true);
    }
}
