using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menutyousei : MonoBehaviour
{
    [SerializeField] GameObject MenuObject;
    bool menuzyoutai;

    // Update is called once per frame
    void Update()
    {
        if (menuzyoutai == false)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                MenuObject.gameObject.SetActive(true);
                menuzyoutai = true;

                // ゲームを停止
                Time.timeScale = 0f;

                // マウスカーソルを表示にし、位置固定解除
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {
            if (Input.GetButtonDown("Cancel"))
            {
                MenuObject.gameObject.SetActive(false);
                menuzyoutai = false;

                // ゲームを再開
                Time.timeScale = 1f;

                // マウスカーソルを非表示にし、位置を固定
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
