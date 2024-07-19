using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    public Animator animator; // アニメーターコンポーネントへの参照
    private bool isButtonClicked = false; // ボタンクリックの追跡用変数

    // ボタンが押されたときに呼び出されるメソッド
    public void OnButtonClick()
    {
        isButtonClicked = true; // ボタンがクリックされたので、フラグをtrueに設定
    }

    void Update()
    {
        if (phase()) // phase関数がtrueを返す場合
        {
            animator.SetBool("IsFphase", true); // アニメーション再生
            isButtonClicked = false; // フラグをリセット
        }
    }

    // phase関数
    private bool phase()
    {
        // ボタンがクリックされたかどうかをチェック
        return isButtonClicked;
    }
}