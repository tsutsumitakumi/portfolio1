using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirecter : MonoBehaviour
{

    public Player1[] playerList;//�v���C���[�̃��X�g
    public bool Movable;//�����邩(�X�^���o�C�t�F�[�Y)
    public bool Summonable;//�����ł��邩(���C���t�F�[�Y)
    public bool Attackable;//�U���ł��邩�i�o�g���t�F�[�Y�j
    public bool Blockable;//�h��ł��邩�i�o�g���t�F�[�Y�j
    public bool Zekkouhyoujun;//���x�߂������ł��邩
    public GameObject phase_text;//�ǂ̃t�F�[�Y����\������
    public GameObject fade_panel;
    public GameObject Ensyutu;

    public GameManager manage_script;
    public CPU cpu_script;
    public GameObject before_outline;
    public GameObject before_outline_object;

    public CardController[] playerHandCardList;//�v���C���[�̎�D���i�[���郊�X�g
    public CardController[] playerFieldCardList;//�t�B�[���h�̃J�[�h���i�[���郊�X�g
    public CardController[] playerkitchenCardList;//������̃J�[�h���i�[���郊�X�g
    public CardController[] enemyHandCardList;//�G�̎�D���i�[���郊�X�g
    public CardController[] EnemyKitchenCardList;//�G�̒�����̃J�[�h���i�[���郊�X�g
    public CardController[] EnemyFieldCardList;//�G�̃t�B�[���h�̃J�[�h���i�[���郊�X�g

    public ObjectHighlight[] SearchImageList;//�T�[�`����J�[�h

    public GameObject p_text, e_text,life_de_ukeru;
    public int turn;
    public bool draw,main, battle;
    public int player_life, enemy_life;//�v���C���[�ƃG�l�~�[�̃��C�t
    public bool enemyattack,playerattack,Koukahatudou,senityuu,enemyblock;

    public TextMeshProUGUI phaseText;// UI�e�L�X�g���A�T�C�����邽�߂̃p�u���b�N�ϐ�
    public Animator phaseAnimator;
    SE_Controller SE;
    public Button _button;
    public CPU _cpu;
    public bool E_turn;
    public bool E_draw;

    public enum Phase//�t�F�[�Y�Ǘ��p�񋓌^�ϐ�

    {
        INIT,
        DRAW,
        STANDBY,
        MAIN,
        BATTLE,
        END,
        Enemy_INIT,
        Enemy_DRAW,
        Enemy_STANDBY,
        Enemy_MAIN,
        Enemy_BATTLE,
        Enemy_END,
    };

    public Phase phase;
    Player1 currentPlayer;
    void Start()
    {
        Ensyutu.SetActive(false);
        SE = GameObject.Find("SE").GetComponent<SE_Controller>();
        phase = Phase.INIT;
        fade_panel = GameObject.Find("Fade");
        fade_panel.SetActive(false);
        life_de_ukeru.SetActive(false);
        Movable = false;
        manage_script = GameObject.Find("GameManager").GetComponent<GameManager>();
        cpu_script = this.gameObject.GetComponent<CPU>();
        main = true;
        battle = true;
        player_life = 5;
        enemy_life = 5;

        _button = GetComponent<Button>();
        _cpu = GameObject.Find("GameDirecter").GetComponent<CPU>();
    }
    private Phase previousPhase;
    void Update()
    {
        p_text.GetComponent<TextMeshProUGUI>().text = player_life.ToString();
        e_text.GetComponent<TextMeshProUGUI>().text = enemy_life.ToString();

        //�J�[�h�̃��X�g�i�[
        playerHandCardList = manage_script.playerHand.GetComponentsInChildren<CardController>();
        playerFieldCardList = manage_script.playerField.GetComponentsInChildren<CardController>();
        playerkitchenCardList = manage_script.playerKitchen.GetComponentsInChildren<CardController>();

        enemyHandCardList = manage_script.enemyHand.GetComponentsInChildren<CardController>();
        EnemyFieldCardList = manage_script.enemyField.GetComponentsInChildren<CardController>();
        EnemyKitchenCardList = manage_script.enemyKitchen.GetComponentsInChildren<CardController>();

        SearchImageList = manage_script.searchArea.GetComponentsInChildren<ObjectHighlight>();

        if(enemy_life <= 0 && !senityuu)
        {
            senityuu = true;
            StartCoroutine("Color_FadeOut");
        }


        switch (phase)
        {
            case Phase.INIT://�����t�F�[�Y
                currentPlayer = playerList[0];
                InitPhase();
                break;
            case Phase.DRAW://�h���[�t�F�[�Y
                Debug.Log("�ǂ�[�ӂ��[����������");
                if (!draw)
                {
                    draw = true;
                    UpdatePhaseText();
                    Debug.Log("�ǂ�[�ӂ��[��");
                    Debug.Log(draw);

                    Invoke("DrawPhase", 3.0f);
                }
                
                E_turn = false;
                break;
            case Phase.STANDBY://�X�^���o�C�i�ړ��j�t�F�[�Y
                draw = false;
                StandbyPhase();
                break;
            case Phase.MAIN://�X�^���o�C�i�ړ��j�t�F�[�Y
                MainPhase();
                break;
            case Phase.BATTLE://�o�g���t�F�[�Y
                BattlePhase();
                break;
            case Phase.END://�G���h�t�F�[�Y
                EndPhase();
                break;
            case Phase.Enemy_DRAW://�h���[�t�F�[�Y
                turn++;
                Enemy_DrawPhase();
                E_turn = true;
                break;
            case Phase.Enemy_STANDBY://�X�^���o�C�i�ړ��j�t�F�[�Y
                Enemy_StandbyPhase();
                break;
            case Phase.Enemy_MAIN://���C���t�F�[�Y
                Enemy_MainPhase();
                break;
            case Phase.Enemy_BATTLE://�o�g���t�F�[�Y
                Enemy_BattlePhase();
                break;
            case Phase.Enemy_END://�G���h�t�F�[�Y
                Enemy_EndPhase();
                cpu_script.bans = false;
                cpu_script.mafin = false;
                break;
        }

        if (phase != previousPhase)
        {
            // �t�F�[�Y�C���f�b�N�X��1�ɐݒ肵�ăA�j���[�V�������g���K�[
            phaseAnimator.SetInteger("phaseIndex", 1);

            // �A�j���[�V�����Đ���A�t�F�[�Y�C���f�b�N�X�����ɖ߂����߂̏������X�P�W���[��
            // �K�v�ɉ����Ēx����ݒ�
            StartCoroutine(ResetPhaseIndexAfterDelay(0.5f));

            // ���݂̃t�F�[�Y���L�^
            previousPhase = phase;
        }
        IEnumerator ResetPhaseIndexAfterDelay(float delay)
        {
            // �w�肳�ꂽ�x����Ɏ��s
            yield return new WaitForSeconds(delay);

            // �t�F�[�Y�C���f�b�N�X�����ɖ߂��i��: 0�Ƀ��Z�b�g�j
            phaseAnimator.SetInteger("phaseIndex", 0);
        }
    }
    void InitPhase()
    {
        Debug.Log("InitPhase");
        // �t�F�[�Y�ύX�ɔ����e�L�X�g�̍X�V
        UpdatePhaseText();
        phase_text.GetComponent<TextMeshProUGUI>().text = currentPlayer+"\nInit";
        phase = Phase.DRAW;
        draw = false;
    }
    void DrawPhase()
    {
        Debug.Log("�ǂ�[�[�[�[�[�[�[�[�[");
        // �t�F�[�Y�ύX�ɔ����e�L�X�g�̍X�V
        phase_text.GetComponent<TextMeshProUGUI>().text = currentPlayer + "\nDraw";
        currentPlayer.Draw();
        for(int i =0;i<playerFieldCardList.Length;i++)
        {
            playerFieldCardList[i].kaihuku();
        }
        phase = Phase.STANDBY;
        manage_script.Buns = false;
        manage_script.Patty = false;
        manage_script.Muffin = false;
        manage_script.Pickles = false;
        manage_script.Foodraw = false;
        manage_script.Plan = false;
        manage_script.Stop = false;
        manage_script.bagamute = false;
        manage_script.chibaga = false;
        manage_script.torabaga = false;
        manage_script.egumahu = false;
    }
    void StandbyPhase()
    {
        Debug.Log("StandbyPhase");
        // �t�F�[�Y�ύX�ɔ����e�L�X�g�̍X�V
        UpdatePhaseText();
        phase_text.GetComponent<TextMeshProUGUI>().text = currentPlayer + "\nStandby";
        Movable = true;

        // StandbyPhase�ɓ������Ƃ��ɃJ�[�h�̌��������Z�b�g
        ResetCardRotation(playerFieldCardList);
    }

    // Player_field�ɂ���J�[�h�̌��������Z�b�g����֐�
    public void ResetCardRotation(CardController[] cards)
    {
        
        foreach (var card in cards)
        {
            if (card != null && phase == Phase.Enemy_STANDBY)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                card.transform.rotation = Quaternion.identity;
            }
        }
    }

    void MainPhase()
    {
        Debug.Log("MainPhase");
        // �t�F�[�Y�ύX�ɔ����e�L�X�g�̍X�V
        UpdatePhaseText();
        Summonable = true;
        phase_text.GetComponent<TextMeshProUGUI>().text = currentPlayer + "\nMain";
    }
    void BattlePhase()
    {
        Debug.Log("BattlePhase");
        // �t�F�[�Y�ύX�ɔ����e�L�X�g�̍X�V
        UpdatePhaseText();
        Movable = false;
        Summonable = false;
        if(turn != 0)
        {
            Attackable = true;
        }

        phase_text.GetComponent<TextMeshProUGUI>().text = currentPlayer + "\nBattle";

        // BATTLE�t�F�[�Y�ɓ������̂ŃN���b�N��������
        foreach (var objClickExample in FindObjectsOfType<ObjectClickExample>())
        {
            objClickExample.EnterBattlePhase();
        }

    }
    void EndPhase()
    {
        Debug.Log("EndPhase");
        E_draw = true;
        // �t�F�[�Y�ύX�ɔ����e�L�X�g�̍X�V
        UpdatePhaseText();
        Attackable = false;
        phase_text.GetComponent<TextMeshProUGUI>().text = currentPlayer + "\nEnd";
        for (int i = 0; i < playerFieldCardList.Length; i++)
        {
            playerFieldCardList[i].power_back();
        }
        //if (currentPlayer == playerList[0])
        //{
        //    currentPlayer = playerList[1];
        //}
        //else
        //{
        //    currentPlayer = playerList[0];
        //}
        Invoke("changeP_end", 3.5f);

        // BATTLE�t�F�[�Y����o���̂ŃN���b�N���֎~����
        foreach (var objClickExample in FindObjectsOfType<ObjectClickExample>())
        {
            objClickExample.ExitBattlePhase();
        }
    }

    void changeP_end()
    {
        
        phase = Phase.Enemy_DRAW;
        
    }

    void Enemy_DrawPhase()
    {
        Debug.Log("Enemy_DrawPhase");
        
        // �t�F�[�Y�ύX�ɔ����e�L�X�g�̍X�V
        UpdatePhaseText();
        phase_text.GetComponent<TextMeshProUGUI>().text = "Enemy" + "\nDraw";
        currentPlayer.EnemyDraw();
        Invoke("Invoke_changeStanby", 3.5f);
    }

    void Invoke_changeStanby()
    {
        phase = Phase.Enemy_STANDBY;
    }

    void Enemy_StandbyPhase()
    {
        Debug.Log("Enemy_StandbyPhase");
        // �t�F�[�Y�ύX�ɔ����e�L�X�g�̍X�V
        UpdatePhaseText();
        phase_text.GetComponent<TextMeshProUGUI>().text = "Enemy" + "\nStandby";
        _cpu.Standby();
        Invoke("Invoke_changeMain", 3.5f);
    }

    void Invoke_changeMain()
    {
        phase = Phase.Enemy_MAIN;
    }

    void Enemy_MainPhase()
    {
        if(main)
        {
            Debug.Log("Enemy_MainPhase");
            // �t�F�[�Y�ύX�ɔ����e�L�X�g�̍X�V
            UpdatePhaseText();
            phase_text.GetComponent<TextMeshProUGUI>().text = "Enemy" + "\nMain";
            //cpu_script.Main(turn);
            cpu_script.Main();
            main = false;
        }
        //phase = Phase.Enemy_BATTLE;
    }

    void Enemy_BattlePhase()
    {
        if(battle)
        {
            Debug.Log("Enemy_BattlePhase");
            // �t�F�[�Y�ύX�ɔ����e�L�X�g�̍X�V
            UpdatePhaseText();
            phase_text.GetComponent<TextMeshProUGUI>().text = "Enemy" + "\nBattle";
            main = true;
            battle = false;
            cpu_script.battle(turn);
        }

        if (manage_script.Stop)
        {
            phase = Phase.Enemy_END;
        }
        Blockable = true;
    }

    void Enemy_EndPhase()
    {
        Debug.Log("Enemy_EndPhase");
        // �t�F�[�Y�ύX�ɔ����e�L�X�g�̍X�V
        UpdatePhaseText();
        phase_text.GetComponent<TextMeshProUGUI>().text = "Enemy" + "\nEnd";        
        battle = true;
        //Invoke("Invoke_draw", 3.5f);
        phase = Phase.DRAW;
        Blockable = false;
    }

    void Invoke_draw()
    {
        Debug.Log("Invoke��ǂ�[�ӂ��[��");

        phase = Phase.DRAW;
        Debug.Log(phase);
    }

    public void Change_Main()
    {
        phase = Phase.Enemy_MAIN;
    }

    public void Change_Battle()
    {
        phase = Phase.Enemy_BATTLE;
    }

    public void Change_End()
    {
        phase = Phase.Enemy_END;

    }

    

    public void NextPhase()
    {
        switch (phase)
        {
            case Phase.STANDBY://�X�^���o�C�i�ړ��j�t�F�[�Y
                phase = Phase.MAIN;
                break;
            case Phase.MAIN://���C���t�F�[�Y
                phase = Phase.BATTLE;
                break;
            case Phase.BATTLE://�o�g���t�F�[�Y
                phase = Phase.END;
                break;
        }
    }

    public void ChangePhase(Phase newPhase)
    {
        phase = newPhase;
        // phaseIndex�p�����[�^���X�V
        if (phaseAnimator != null)
        {
            phaseAnimator.SetInteger("phaseIndex", (int)phase);
        }
        UpdatePhaseText(); // �t�F�[�Y�e�L�X�g�̍X�V
        TriggerPhaseAnimation(); // �t�F�[�Y�A�j���[�V�����̃g���K�[
    }

    void TriggerPhaseAnimation()
{
    // �A�j���[�V�����̃g���K�[���� 'Phase' �ɍ��킹�ăg���K�[����
    phaseAnimator.SetTrigger("Phase");
}



    void UpdatePhaseText()
    {
        Color enemyPhaseColor = new Color(255f / 255f, 14f / 255f, 14f / 255f,255f / 255f); // �G�̃t�F�[�Y�̐F��Ԃɐݒ�
        Color playerPhaseColor = new Color(55f / 255f, 140f / 255f, 212f / 255f, 255f / 255f); // �v���C���[�̃t�F�[�Y�̐F��ɐݒ�
        switch (phase)
        {
            // �v���C���[�t�F�[�Y�̏ꍇ�A�e�L�X�g�̐F��F�ɐݒ�
            case Phase.DRAW:
                phaseText.text = "Draw Phase";
                phaseText.color = playerPhaseColor; // �F
                break;
            case Phase.STANDBY:
                phaseText.text = "Standby Phase";
                phaseText.color = playerPhaseColor; // �F
                break;
            case Phase.MAIN:
                phaseText.text = "Main Phase";
                phaseText.color = playerPhaseColor;  // �F
                break;
            case Phase.BATTLE:
                phaseText.text = "Battle Phase";
                phaseText.color = playerPhaseColor;  // �F
                break;
            case Phase.END:
                phaseText.text = "End Phase";
                phaseText.color = playerPhaseColor;  // �F
                break;

            // �G�̃t�F�[�Y�̏ꍇ�A�e�L�X�g�̐F��ԐF�ɐݒ�
            case Phase.Enemy_DRAW:
                phaseText.text = "Draw Phase";
                phaseText.color = enemyPhaseColor; // �ԐF
                break;
            case Phase.Enemy_STANDBY:
                phaseText.text = "Standby Phase";
                phaseText.color = enemyPhaseColor; // �ԐF
                break;
            case Phase.Enemy_MAIN:
                phaseText.text = "Main Phase";
                phaseText.color = enemyPhaseColor; // �ԐF
                break;
            case Phase.Enemy_BATTLE:
                phaseText.text = "Battle Phase";
                phaseText.color = enemyPhaseColor; // �ԐF
                break;
            case Phase.Enemy_END:
                phaseText.text = "End Phase";
                phaseText.color = enemyPhaseColor; // �ԐF
                break;
            default:
                phaseText.text = "Undefined Phase";
                phaseText.color = Color.white; // �f�t�H���g�̐F
                break;
        }
    }

    IEnumerator Destroy_me(GameObject me)
    {
        SE.hakai_SE();
        yield return new WaitForSeconds(0.5f);
        Destroy(me);
    }

    IEnumerator Destroy_me_ETurn(GameObject me)
    {
        SE.hakai_SE();
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Destroy_me_ETrun�̌Ăяo��");
        Destroy(me);
        Debug.Log("�ł��Ƃ낢");
        Invoke("E_judge", 0.1f);
    }

    void E_judge()
    {
        _cpu.EnemyAttackJudge();
    }

    public void Ensyutu_Start()
    {
        Ensyutu.SetActive(true);
    }

    public void Ensyutu_End()
    {
        Ensyutu.SetActive(false);
    }

    public void Battle(GameObject attack,GameObject block)
    {
        int attack_power = attack.GetComponent<CardView>().power;
        int block_power = block.GetComponent<CardView>().power;
        Debug.Log("�o�g���J�n");
        Debug.Log("�A�^�b�N�p���[" + attack_power);
        Debug.Log("�u���b�N�p���[" + block_power);
        if (phase == Phase.Enemy_BATTLE)
        {
            Debug.Log("�G�l�~�[�o�g���t�F�C�Y");
            if (attack_power > block_power)
            {
                Debug.Log("�A�^�b�J�[�̏���");
                StartCoroutine(Destroy_me_ETurn(block));
            }
            else if (attack_power < block_power)
            {
                Debug.Log("�u���b�J�[�̏���");
                _cpu.AtkCnt -= 1;
                StartCoroutine(Destroy_me_ETurn(attack));
            }
            else if (attack_power == block_power)
            {
                Debug.Log("��������");
                _cpu.AtkCnt -= 1;
                StartCoroutine(Destroy_me(attack));
                StartCoroutine(Destroy_me_ETurn(block));
            }
        }
        else
        {
            if (attack_power > block_power)
            {
                Debug.Log("�A�^�b�J�[�̏���");
                StartCoroutine(Destroy_me(block));
            }
            else if (attack_power < block_power)
            {
                Debug.Log("�u���b�J�[�̏���");
                StartCoroutine(Destroy_me(attack));
            }
            else if (attack_power == block_power)
            {
                Debug.Log("��������");
                StartCoroutine(Destroy_me(attack));
                StartCoroutine(Destroy_me(block));
            }
        }

        attack.GetComponent<CardController>().attack = false;
        block.GetComponent<CardController>().block = false;
        playerattack = false;
        enemyattack = false;
    }

    IEnumerator Color_FadeOut()
    {
        fade_panel.SetActive(true);
        // ��ʂ��t�F�[�h�A�E�g������R�[���`��
        // �O��F��ʂ𕢂�Panel�ɃA�^�b�`���Ă���

        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = fade_panel.GetComponent<Image>();

        // �t�F�[�h��̐F��ݒ�i���j���ύX��
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 255.0f));

        // �t�F�[�h�C���ɂ����鎞�ԁi�b�j���ύX��
        const float fade_time = 0.3f;

        // ���[�v�񐔁i0�̓G���[�j���ύX��
        const int loop_count = 10;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // �҂�����
            yield return new WaitForSeconds(wait_time);

            // Alpha�l���������グ��
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        // Color_FadeOut ������A1�b�҂��Ă��� Color_FadeIn ���s
        yield return new WaitForSeconds(1f);
        if(enemy_life <= 0)
        {
            Win();
        }
        else if (player_life >= 0)
        {
            Lose();
        }
    }

    private void Win()
    {
        SceneManager.LoadScene("WinScene");
    }

    private void Lose()
    {
        SceneManager.LoadScene("LoseScene");
    }
}
