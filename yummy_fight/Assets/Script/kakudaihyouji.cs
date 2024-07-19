using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class kakudaihyouji : MonoBehaviour, IPointerClickHandler
{
    public string targetTag = "Card"; // �g��\������I�u�W�F�N�g�̃^�O
    private RectTransform targetPanel; // �g��\������p�l��
    private GameObject currentObjectInstance; // ���ݕ\������Ă���I�u�W�F�N�g�̃C���X�^���X

    void Start()
    {
        // kakudai �p�l�����������ĎQ�Ƃ���
        GameObject kakudaiPanel = GameObject.Find("Kakudai");
        if (kakudaiPanel != null)
        {
            targetPanel = kakudaiPanel.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("kakudai �p�l����������܂���ł����B");
        }

        // �V�[����EventSystem���Ȃ��ꍇ�͒ǉ�����
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerPress;
        if (clickedObject != null && clickedObject.CompareTag(targetTag))
        {
            // �g��\������I�u�W�F�N�g���N���b�N���ꂽ�ꍇ
            if (currentObjectInstance == null)
            {
                Debug.Log("�g��");
                currentObjectInstance = Instantiate(clickedObject, targetPanel); // �N���b�N���ꂽ�I�u�W�F�N�g�̃R�s�[���쐬
                currentObjectInstance.transform.SetParent(targetPanel); // �g��\������p�l���̎q�ɂ���
                currentObjectInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // �p�l���̒����ɔz�u
                currentObjectInstance.GetComponent<RectTransform>().localScale = Vector3.one * 2f; // 2�{�̃T�C�Y�Ɋg��
            }
            else
            {
                Debug.Log("�폜");
                Destroy(currentObjectInstance); // ���łɕ\������Ă���ꍇ�͍폜
                currentObjectInstance = null;
            }
        }
        else
        {
            // �g��\������I�u�W�F�N�g�ȊO���N���b�N���ꂽ�ꍇ
            if (currentObjectInstance != null)
            {
                Debug.Log("�폜�Q");
                Destroy(currentObjectInstance); 
                currentObjectInstance = null;
            }
        }
    }
}
