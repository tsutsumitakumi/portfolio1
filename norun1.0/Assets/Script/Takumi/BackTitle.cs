using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�@�^�C�g���ɖ߂�
public class BackTitle : MonoBehaviour
{
    public void Back_button()
    {
        // �uTitle�v�����[�h
        SceneManager.LoadScene("Title");
    }
}
