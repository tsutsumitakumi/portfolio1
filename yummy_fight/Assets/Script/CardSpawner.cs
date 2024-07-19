using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CardSpawner : MonoBehaviourPunCallbacks
{
    public GameObject cardPrefab; // �J�[�h�̃v���n�u

    // �J�[�h�����̃t�B�[���h�G���A�ɐ������郁�\�b�h
    public void SpawnCardOnField(int cardID, Vector3 position, Quaternion rotation, bool isFaceUp, bool isOpponentsField)
    {
        // �J�[�h�̃f�[�^���܂ރI�v�V���i���ȃp�����[�^
        object[] cardData = new object[] { cardID, isFaceUp };

        // �J�[�h���C���X�^���X�����A�S�N���C�A���g�œ���
        GameObject card = PhotonNetwork.Instantiate(cardPrefab.name, position, rotation, 0, cardData);

        // ����̃t�B�[���h�ɔz�u����ꍇ�͈ʒu�ƌ����𒲐�
        if (isOpponentsField)
        {
            // ��: ����̃t�B�[���h�ɃJ�[�h��\�����邽�߂̈ʒu�ƌ����̒���
            card.transform.Rotate(0f, 180f, 0f); // �J�[�h��180�x��]�����đ���Ɍ�����
        }
    }
}
