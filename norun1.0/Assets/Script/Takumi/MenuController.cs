using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuObject;

    void Start()
    {
        // �ŏ��̓��j���[���\���ɂ���
        menuObject.SetActive(false);
    }

    public void ToggleMenu()
    {
        // ���j���[�̕\���E��\����؂�ւ���
        menuObject.SetActive(!menuObject.activeSelf);
    }
}
