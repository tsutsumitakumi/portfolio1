using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardController : MonoBehaviour
{
    // �J�[�h�̌����ڂ��Ǘ�����R���|�[�l���g
    public CardView view;
    // �J�[�h�̃f�[�^���Ǘ����郂�f��
    public CardModel model;
    // �J�[�h�̏�ԃt���O
    public bool hirou, attack, block, egumahu_aru;

    // �e��Q�[���Ǘ��̎Q��
    private GameDirecter _directer;
    private GameManager _manager;
    private SE_Controller SE;

    // UI�{�^���̎Q��
    public GameObject attack_button, blockbutton, kouka_button;
    // �J�[�h�̃p���[�e�L�X�g
    public GameObject power_text;
    // �f�t�H���g�̃p���[�l
    public int default_power;

    private void Awake()
    {
        // �e�R���|�[�l���g�̎Q�Ƃ��擾
        view = GetComponent<CardView>();
        SE = GameObject.Find("SE").GetComponent<SE_Controller>();
        default_power = view.power;
        hirou = false;
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // �e�{�^���̎Q�Ƃ��擾
        attack_button = transform.Find("Attack").gameObject;
        blockbutton = transform.Find("Block").gameObject;
        kouka_button = transform.Find("Kouka").gameObject;

        // �u���b�N�{�^���ƌ��ʃ{�^�����\���ɂ���
        blockbutton.SetActive(false);
        kouka_button.SetActive(false);
    }

    void Update()
    {
        // �G�̍U���������Ăяo��
        if (_directer.enemyattack)
        {
            enemyattack();
        }

        // �J�[�h�̃p���[�e�L�X�g���X�V
        power_text.GetComponent<TextMeshProUGUI>().text = view.power.ToString();

        // �o�g���t�F�[�Y�̏���
        if (_directer.phase == GameDirecter.Phase.BATTLE || _directer.phase == GameDirecter.Phase.Enemy_BATTLE)
        {
            int cnt = 0;
            for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
            {
                if (_directer.playerFieldCardList[i].hirou && !_manager.egumahu)
                {
                    egumahu_aru = true;
                    cnt++;
                }
            }

            if (cnt == 0)
            {
                egumahu_aru = false;
            }
        }

        // �u���b�N�{�^���̕\��/��\���̐���
        if (hirou)
        {
            blockbutton.SetActive(false);
        }
        else if (!hirou && _directer.phase == GameDirecter.Phase.Enemy_BATTLE
                 && GetComponent<CardMovement>().cardParent == GameObject.Find("Player_field").transform)
        {
            blockbutton.SetActive(true);
        }

        // ���ʃ{�^���̕\��/��\���̐���
        if ((_directer.Attackable || _directer.Blockable)
            && view.cardID == 102
            && !hirou
            && egumahu_aru)
        {
            kouka_button.SetActive(true);
        }
        else
        {
            kouka_button.SetActive(false);
        }
    }

    public void Init(int cardID)�@// �J�[�h�𐶐��������ɌĂ΂��֐�
    {
        Debug.Log(cardID);
        view.cardID = cardID;
        model = new CardModel(cardID); // �J�[�h�f�[�^�𐶐�
        view.Show(model); // �J�[�h��\��
    }

    // �J�[�h��j��
    public void Destroy_me()
    {
        Destroy(gameObject);
    }

    // �J�[�h��90�x��]�����܂��B
    public void RotateCard()
    {
        transform.Rotate(new Vector3(0f, 0f, 90f));
        transform.localScale = new Vector3(3.5f, 0.8f, 1.3f);
        hirou = true;
    }

    // �J�[�h�̃p���[���f�t�H���g�ɖ߂�
    public void power_back()
    {
        view.power = default_power;
    }

    // �G�̍U������
    public void enemyattack()
    {
        transform.Rotate(new Vector3(0f, 0f, -90f));
        transform.localScale = new Vector3(3.5f, 0.8f, 1.3f);
        hirou = true;
        attack = true;
        _directer.Zekkouhyoujun = true;
        Player_Block();
        _directer.life_de_ukeru.SetActive(true);
    }

    // �G�̃u���b�N����
    public void enemyblock()
    {
        if (!hirou)
        {
            Debug.Log("CPU�F�u���b�N���܂�");
            transform.Rotate(new Vector3(0f, 0f, -90f));
            transform.localScale = new Vector3(3.5f, 0.8f, 1.3f);
            block = true;
            hirou = true;

            for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
            {
                if (_directer.playerFieldCardList[i].attack)
                {
                    _directer.Battle(_directer.playerFieldCardList[i].gameObject, gameObject);
                }
            }
        }
    }

    // �v���C���[�̃u���b�N����
    public void Player_Block()
    {
        for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
        {
            if (!_directer.playerFieldCardList[i].hirou)
            {
                _directer.playerFieldCardList[i].GetComponent<CardController>().blockbutton.SetActive(true);
            }
        }
    }

    // �J�[�h���񕜁i�J�[�h���c�����ɂ���j
    public void kaihuku()
    {
        if (hirou)
        {
            transform.Rotate(new Vector3(0f, 0f, -90f));
            transform.localScale = new Vector3(1.3f, 2f, 1.3f);
            hirou = false;
        }
    }

    // �J�[�h���J��Ԃɂ���i�J�[�h���������ɂ���j
    public void Hirou()
    {
        if (!hirou)
        {
            transform.Rotate(new Vector3(0f, 0f, 90f));
            transform.localScale = new Vector3(3.5f, 0.8f, 1.3f);
            hirou = true;
        }
    }

    // �G�̃J�[�h���񕜁i�J�[�h���c�����ɂ���j
    public void kaihuku_Enemy()
    {
        if (hirou)
        {
            transform.Rotate(new Vector3(0f, 0f, 90f));
            transform.localScale = new Vector3(1.3f, 2f, 1.3f);
            hirou = false;
        }
    }
}
