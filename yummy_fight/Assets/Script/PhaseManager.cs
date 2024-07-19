using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhaseManager : MonoBehaviour
{
    public TextMeshProUGUI phaseText; // UI�e�L�X�g���A�T�C�����邽�߂̃p�u���b�N�ϐ�

    // �t�F�[�Y�������񋓌^
    public enum GamePhase
    {
        MainPhase,
        BattlePhase,
        // �K�v�ɉ����đ��̃t�F�[�Y��ǉ�
    }

    // ���݂̃t�F�[�Y��ݒ肵�܂��B�ŏ��̓��C���t�F�[�Y�Ƃ��܂��B
    private GamePhase currentPhase = GamePhase.MainPhase;

    // �t�F�[�Y��ύX���郁�\�b�h
    public void ChangePhase(GamePhase newPhase)
    {
        currentPhase = newPhase;
        UpdatePhaseText();
    }

    // UI�e�L�X�g���X�V���郁�\�b�h
    void UpdatePhaseText()
    {
        switch (currentPhase)
        {
            case GamePhase.MainPhase:
                phaseText.text = "���C���t�F�[�Y";
                break;
            case GamePhase.BattlePhase:
                phaseText.text = "�o�g���t�F�[�Y";
                break;
                // �K�v�ɉ����đ��̃t�F�[�Y�̏�����ǉ�
        }
    }
}