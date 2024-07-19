using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1kako : MonoBehaviour
{
    private bool kako;
    private bool mirai;
    private bool ima;

    public Animator _anim;

    [SerializeField] GameObject clockani;
    [SerializeField, Header("�C���{�[�N����")]
    float wait;


    private void Start()
    {
        _anim = GameObject.Find("clock_A").GetComponent<Animator>();
        kako = false;
        mirai = false;
        ima = false;
    }

    private void Update()
    {
        void OnClick()
        {
           
            Debug.Log("�N���b�N");
            //Invoke("change_button", 2);
            //change_button();
            // �Q�[�����ĊJ
            /*Time.timeScale = 1f;
            Debug.Log("�{�^��");
            SceneManager.LoadScene("Stage1kako");*/
        }
    }


    void change_button()
    {
        kako = true;
        //clockani.SetActive(true);
        Debug.Log("�A�j���[�V����"+kako);
        _anim.SetBool("kako", kako);
        Invoke("stage_change", wait);
        Debug.Log("����ځ[��");

    }
    
    void stage_change()
    {
        kako = false;
        // �Q�[�����ĊJ
        Time.timeScale = 1f;
        Debug.Log("�{�^��");
        SceneManager.LoadScene("Stage1kako");
    }
}
