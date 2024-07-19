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

                // �Q�[�����~
                Time.timeScale = 0f;

                // �}�E�X�J�[�\����\���ɂ��A�ʒu�Œ����
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

                // �Q�[�����ĊJ
                Time.timeScale = 1f;

                // �}�E�X�J�[�\�����\���ɂ��A�ʒu���Œ�
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
