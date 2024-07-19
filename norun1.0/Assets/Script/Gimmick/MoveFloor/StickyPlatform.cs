using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StickyPlatform : MonoBehaviour
{
    // ���̏㑤�R���C�_�[�̒��ɓ������Ƃ��Ɏ��s
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �Փ˂����I�u�W�F�N�g����Player�Ȃ�A���̎q�I�u�W�F�N�g�ɂ���
        if (collision.gameObject.name == "norun")
        {
            collision.gameObject.transform.SetParent(transform);

        }
    }
    // ���̏㑤�R���C�_�[���痣�ꂽ�Ƃ��Ɏ��s
    private void OnTriggerExit2D(Collider2D collision)
    {
        // �Փ˂����I�u�W�F�N�g����Player�Ȃ�A���̎q�I�u�W�F�N�g�����������
        if (collision.gameObject.name == "norun")
        {
            collision.gameObject.transform.SetParent(null);
            DontDestroyOnLoad(collision.gameObject);

        }
    }
}
