using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timetyousei : MonoBehaviour
{
    [SerializeField] GameObject MenuObject;
    [SerializeField] GameObject MenuObject2;
    [SerializeField] GameObject MenuObject3;
    //[SerializeField] GameObject MenuObject4;

    bool menuzyoutai;

    void Start()
    {
        MenuObject2.SetActive(false);
        MenuObject3.SetActive(false);
        //MenuObject4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ���j���[�̐؂�ւ�();
            Debug.Log(menuzyoutai);
           
        }
    }

    void ���j���[�̐؂�ւ�()
    {
        //menuzyoutai = !menuzyoutai;

        if (menuzyoutai == true)
        {

            // �Q�[�����ĊJ
            Time.timeScale = 1f;



            // ���j���[��\��
            MenuObject.SetActive(true);

            MenuObject2.SetActive(false);

            MenuObject3.SetActive(false);

            //MenuObject4.SetActive(false);

            menuzyoutai = false;
            // �}�E�X�J�[�\����\���ɂ��A�ʒu�Œ����
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // �Q�[�����~
            //Time.timeScale = 0f;


            menuzyoutai = true;
            // ���j���[���\��
            MenuObject.SetActive(false);

            MenuObject2.SetActive(true);

            MenuObject3.SetActive(true);

            //MenuObject4.SetActive(true);

            // �}�E�X�J�[�\�����\���ɂ��A�ʒu���Œ�
            /*Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;*/
        }
    }
}

