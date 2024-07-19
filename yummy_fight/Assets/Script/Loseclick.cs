using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Loseclick : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        // TextMeshPro�R���|�[�l���g���擾
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // ������Ԃł͔�\���ɂ���
        textMeshPro.gameObject.SetActive(false);

        // 4�b��ɕ\������
        Invoke("ShowTextMeshPro", 4f);
    }

    void ShowTextMeshPro()
    {
        // TextMeshPro��\������
        textMeshPro.gameObject.SetActive(true);
    }
}
