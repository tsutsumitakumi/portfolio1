using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5; // �ő�̗�
    public int currentHealth; // ���݂̗̑�
    public GameObject blockButton; // �u���b�N�{�^��
    public GameObject lifeButton; // ���C�t�{�^��

    void Start()
    {
        currentHealth = maxHealth; // �Q�[���J�n���Ɍ��݂̗̑͂��ő�̗͂ɐݒ�
    }

    public void TakeDamage(int damageAmount)
    {
        if (!blockButton.activeSelf) // �u���b�N�{�^������A�N�e�B�u�̏ꍇ
        {
            blockButton.SetActive(true); // �u���b�N�{�^����\������
        }

        if (!lifeButton.activeSelf) // ���C�t�{�^������A�N�e�B�u�̏ꍇ
        {
            lifeButton.SetActive(true); // ���C�t�{�^����\������
        }

        currentHealth -= damageAmount; // �_���[�W���󂯂����������݂̗̑͂����炷

        if (currentHealth <= 0)
        {
            Die(); // �̗͂�0�ȉ��ɂȂ����玀�S���������s
        }
    }

    void Die()
    {
        // �v���C���[�����S�����ۂ̏����������ɋL�q
        // �Ⴆ�΁A�Q�[���I�[�o�[��ʂ�\������A���X�|�[������Ȃǂ̏������L�q
        Debug.Log("Player died!");
    }
}
