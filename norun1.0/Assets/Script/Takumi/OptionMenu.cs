using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// オプションメニュー
public class OptionMenu : MonoBehaviour
{
    public void Menu_button()
    {
        //「Option」シーンのロード
        SceneManager.LoadScene("Option");
    }
}
