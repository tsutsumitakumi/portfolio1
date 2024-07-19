using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    public GameObject attackButton;
    public bool cardSelected = false;

    void Start()
    {
        // �ŏ��͍U���{�^�����\���ɂ���
        attackButton.SetActive(false);
    }

    void Update()
    {
        // �}�E�X�̍��N���b�N�������ꂽ���ǂ������m�F���A�J�[�h���N���b�N���ꂽ�ꍇ�Ƀt���O��ݒ肷��
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Raycast�̉����p�ɁARay�̎n�_�ƏI�_���v�Z
            Vector2 origin = transform.position; // �J�[�h�̈ʒu���n�_�Ƃ���
            Vector2 direction = mousePosition - origin; // �J�[�h����}�E�X�ʒu�ւ̕������I�_�Ƃ���

            // Raycast�����s
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Card"))
                {
                    cardSelected = true;
                    Debug.Log("Card Clicked: " + hit.collider.gameObject.name); // �f�o�b�O���O�ŃN���b�N���ꂽ�J�[�h�̖��O��\��
                    OnCardHit(); // �J�[�h���N���b�N���ꂽ���̒ǉ��̏��������s
                    break;
                }
            }

            // Ray�̉���
            Debug.DrawLine(origin, origin + direction, Color.red, 0.1f);
        }

        // �J�[�h���I������Ă��邩�ǂ������m�F���A�U���{�^���̕\����Ԃ��X�V����
        attackButton.SetActive(cardSelected);
    }

    // �J�[�h���N���b�N���ꂽ���̏���
    void OnCardHit()
    {
        // �J�[�h���N���b�N���ꂽ���̒ǉ��̏����������ɋL�q����
    }
}
