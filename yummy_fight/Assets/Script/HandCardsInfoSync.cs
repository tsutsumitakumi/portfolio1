using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HandCardsInfoSync : MonoBehaviourPun
{
    private GameDirecter gameDirecter; // �ϐ��������N�G�X�g�ɍ��킹�ďC��

    void Start()
    {
        GameObject gameDirecterObject = GameObject.Find("GameDirecter"); // GameObject.Find�̈�����"GameDirecter"�ɕύX
        if (gameDirecterObject != null)
        {
            gameDirecter = gameDirecterObject.GetComponent<GameDirecter>(); // GameObject����GameDirecter�R���|�[�l���g���擾

            // GameDirecter�R���|�[�l���g���擾�ł����ꍇ�Ƀf�o�b�O���O���o��
            if (gameDirecter != null)
            {
                Debug.Log("����������������������");
            }
            else
            {
                Debug.LogError("GameDirecter component not found on the object.");
            }
        }
        else
        {
            Debug.LogError("GameDirecter object not found in the scene."); // ��ѐ��̂��߂̃G���[���b�Z�[�W���X�V
        }
    }

    public void SyncHandCardsCount()
    {
        Debug.Log("SyncHandCardsCount���Ăяo����܂����B");
        int count = gameDirecter.playerHandCardList.Length; // ���ɁAplayerHandCardList����D�̃J�[�h���X�g��ێ�����z�񂾂Ƃ��܂��B
        photonView.RPC("UpdateOpponentHandCardsCount", RpcTarget.Others, count);

        // ��D�������f�o�b�O���O�ɕ\��
        Debug.Log($"��D�̖�����{count}���ł��B");
    }

    [PunRPC]
    void UpdateOpponentHandCardsCount(int count)
    {
        // �����ő��葤��UI���X�V���āA����̎�D������\�����܂��B
    }
}