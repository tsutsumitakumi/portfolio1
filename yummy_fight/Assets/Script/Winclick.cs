using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Winclick : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        // TextMeshPro�R���|�[�l���g���擾
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // ������Ԃł͔�\���ɂ���
        textMeshPro.gameObject.SetActive(false);

        // 4�b��ɕ\������
        Invoke("ShowTextMeshPro", 3.5f);
    }

    void ShowTextMeshPro()
    {
        // TextMeshPro��\������
        textMeshPro.gameObject.SetActive(true);
    }
}
