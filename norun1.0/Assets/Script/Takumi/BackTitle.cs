using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//　タイトルに戻る
public class BackTitle : MonoBehaviour
{
    public void Back_button()
    {
        // 「Title」をロード
        SceneManager.LoadScene("Title");
    }
}
