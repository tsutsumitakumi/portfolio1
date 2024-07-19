using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public GameObject cardObject;
    public GameDirecter _directer;
    public CardController _controller;
    private Button attackButton;
    public bool hirou;
    private SE_Controller SE;

    void Start()
    {
        // �Q�[���f�B���N�^�[�̎Q�Ƃ��擾
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        // SE�R���g���[���[�̎Q�Ƃ��擾
        SE = GameObject.Find("SE").GetComponent<SE_Controller>();
        // �J�[�h�R���g���[���[�̎Q�Ƃ��擾
        _controller = cardObject.GetComponent<CardController>();
        // �{�^���R���|�[�l���g���擾
        attackButton = GetComponent<Button>();
        // �{�^�����\���ɂ���
        this.gameObject.SetActive(false);
    }

    public void OnAttackButtonClick()
    {
        Debug.Log("����ɍU������");
        // �v���C���[�̍U���t���O��ݒ�
        _directer.playerattack = true;
        // �J�[�h�̍U���t���O��ݒ�
        cardObject.GetComponent<CardController>().attack = true;
        // �J�[�h���������ɂ��郁�\�b�h���Ăяo��
        _controller.RotateCard();
        // �v���C���[�ɍU�����郁�\�b�h���Ăяo��
        AttackPlayer();
        // �{�^�����\���ɂ���
        gameObject.SetActive(false);
    }

    void AttackPlayer()
    {
        // �J�[�hID��104�̏ꍇ�A���ʂȌ��ʂ𔭓�
        if (cardObject.GetComponent<CardView>().cardID == 104)
        {
            Debug.Log("�`�[�o�K���ʔ���");
            cardObject.GetComponent<EX_Card_Ability>().StartCoroutine("Chibaga");
            _directer.Koukahatudou = true;
        }

        // �G�̃t�B�[���h�ɃJ�[�h���Ȃ��ꍇ�A�U�����L�����Z�����G�̃��C�t�����炷
        if (_directer.EnemyFieldCardList.Length == 0)
        {
            cardObject.GetComponent<CardController>().attack = false;
            _directer.playerattack = false;
            _directer.enemy_life--;
        }
    }
}
