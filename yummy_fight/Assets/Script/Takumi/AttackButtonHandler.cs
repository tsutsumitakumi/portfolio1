using UnityEngine;
using UnityEngine.UI;

public class AttackButtonHandler : MonoBehaviour
{
    public GameObject attackButtonObject; // AttackButton��GameObject
    public GameObject blockButton; // �u���b�N�{�^��
    public GameObject lifeButton; // ���C�t�Ŏ󂯂�{�^��

    void Start()
    {
        // ������Ԃł̓u���b�N�{�^���ƃ��C�t�{�^�����\���ɂ���
        blockButton.SetActive(false);
        lifeButton.SetActive(false);
    }

    public void OnAttackButtonPressed()
    {
        // �U���{�^���������ꂽ���̏���

        // AttackButton���A�N�e�B�u�ł���ꍇ�̂݁A�u���b�N�{�^���ƃ��C�t�{�^����\������
        if (attackButtonObject.activeSelf)
        {
            blockButton.SetActive(true);
            lifeButton.SetActive(true);
        }
    }
}
