using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{

    bool floor_touch; //���ɐG�ꂽ���̔���
    public float downSpeed; //������X�s�[�h
    public float downsecond; //������܂ł̕b��
    float fallCount; //����������܂ł̎���
    Rigidbody2D rb; //Rigidbody�̐錾

    // �Q�[���J�n
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Rigidbody�̎擾
        fallCount = 0; //fullCpunt��������
    }

    //�X�V����
    void Update()
    {
        //���ɐG�ꂽ��
        if (floor_touch == true)
        {
            //fallCount��1�b�����₷�B
            fallCount += Time.deltaTime;
            //DownStart�֐����g��
            DownStart();
        }

    }

    //�����蔻��(3D�̏ꍇ��2D�͏����Ȃ�)
    private void OnCollisionEnter2D(Collision2D col)
    {
        //�v���C���[�^�O���t���Ă���I�u�W�F�N�g�ɓ���������
        if (col.gameObject.tag == "Player")
        {
            fallCount = 0; //fallCount��������
            floor_touch = true; //floor_touch��true�ɂ���B
        }
    }

    //���b��ɏ���������
    void DownStart()
    {
        //fallCount�����b����������
        if (fallCount >= downsecond)
        {
            transform.Translate(0, downSpeed, 0); //Y���W��downSpeed���ς���B
        }
    }

}

