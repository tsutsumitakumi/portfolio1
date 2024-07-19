using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Synchronization : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject enemyField; // Enemy_field �G���A���A�T�C��

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void SummonCard(int cardID)
    {
        // �J�[�hID�𑊎�ɑ��M
        photonView.RPC("ReceiveCardID", RpcTarget.Others, cardID);
    }

    // �l�b�g���[�N�z���ɌĂяo����郁�\�b�h
    [PunRPC]
    private void ReceiveCardID(int cardID)
    {
        // �J�[�h�̃v���n�u���C���X�^���X��
        GameObject cardObject = PhotonNetwork.Instantiate("CardPrefab", Vector3.zero, Quaternion.identity);

        // �C���X�^���X�����ꂽ�I�u�W�F�N�g���� Card �N���X���擾�����ɁA���ڕ\���������Ă�
        DisplayCardInformation(cardObject, cardID);
    }

    // �J�[�h�𑊎�̃t�B�[���h�Ɉړ������郁�\�b�h
    private void MoveCardToEnemyField(GameObject cardObject)
    {
        // �J�[�h������ Transform �R���|�[�l���g���擾
        Transform cardTransform = cardObject.transform;

        // ����̃t�B�[���h��Ɉړ�������
        cardTransform.SetParent(enemyField.transform);

        // �ʒu���]�Ȃǂ̒������K�v�ł���΁A�K�؂ɐݒ肷��
        cardTransform.localPosition = Vector3.zero; // ��Ƃ��Ē����ɔz�u
        cardTransform.localRotation = Quaternion.identity; // ��]��������
        cardTransform.localScale = Vector3.one; // �X�P�[����������
    }
    // �J�[�h�̏���\�����郁�\�b�h
    private void DisplayCardInformation(GameObject cardObject, int cardID)
    {
        // �J�[�h������ CardEntity ���擾
        CardEntity cardEntity = GetCardEntity(cardID);

        if (cardEntity != null)
        {
            // �J�[�h�I�u�W�F�N�g������ SpriteRenderer �R���|�[�l���g���擾
            SpriteRenderer spriteRenderer = cardObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                // CardEntity ����A�C�R�����擾���� SpriteRenderer �ɐݒ�
                spriteRenderer.sprite = cardEntity.icon;
            }
            else
            {
                Debug.LogError("SpriteRenderer not found on the card object.");
            }
        }
        else
        {
            Debug.LogError("CardEntity not found for card ID: " + cardID);
        }
    }

    // CardID�ɑΉ�����CardEntity���擾���郁�\�b�h
    private CardEntity GetCardEntity(int cardID)
    {
        // �����Ɏ��ۂ̃f�[�^�擾������ǉ�
        // ���̃T���v���ł� ScriptableObject ���g�p���Ă��邽�߁AResources �t�H���_����擾�����������܂��B
        // ���ۂ̃v���W�F�N�g�ł́A�f�[�^�x�[�X��ʂ̃f�[�^�Ǘ��N���X�𗘗p���邱�Ƃ���ʓI�ł��B

        string cardEntityPath = "CardEntities/CardEntity_" + cardID; // ���\�[�X�̃p�X
        CardEntity cardEntity = Resources.Load<CardEntity>(cardEntityPath);

        if (cardEntity != null)
        {
            return cardEntity;
        }
        else
        {
            Debug.LogError("CardEntity not found for card ID: " + cardID);
            return null;
        }
    }
}