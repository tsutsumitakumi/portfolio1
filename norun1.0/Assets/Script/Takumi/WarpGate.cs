using UnityEngine;

public class WarpGate : MonoBehaviour
{
    // ���[�v��̈ʒu
    public Transform warpDestination;

    // ���[�v�����s����L�[
    public KeyCode warpKey = KeyCode.W;

    // ���[�v�����s����Ώۂ̃I�u�W�F�N�g
    public GameObject targetObject;

    private void Update()
    {
        // �L�[�������ꂽ���ǂ������m�F
        if (Input.GetKeyDown(warpKey))
        {
            // �^�[�Q�b�g�I�u�W�F�N�g�̑O�ŃL�[�������ꂽ���ǂ������m�F
            if (IsPlayerInFrontOfGate())
            {
                // ���[�v�����s
                WarpPlayer();
            }
        }
    }

    private bool IsPlayerInFrontOfGate()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target object not set");
            return false;
        }

        // �^�[�Q�b�g�I�u�W�F�N�g�̑O�Ƀv���C���[�����邩�ǂ������m�F
        Collider2D targetCollider = targetObject.GetComponent<Collider2D>();
        if (targetCollider == null)
        {
            Debug.LogWarning("Target object does not have a Collider2D component");
            return false;
        }

        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Collider2D>();
        if (playerCollider == null)
        {
            Debug.LogWarning("Player object not found");
            return false;
        }

        return Physics2D.IsTouching(targetCollider, playerCollider);
    }

    private void WarpPlayer()
    {
        // �v���C���[�����[�v��̈ʒu�Ɉړ�������
        if (warpDestination != null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                Transform playerTransform = playerObject.transform;
                playerTransform.position = warpDestination.position;
                Debug.Log("Player warped");
            }
            else
            {
                Debug.LogWarning("Player not found");
            }
        }
        else
        {
            Debug.LogWarning("Warp destination not set");
        }
    }
}

