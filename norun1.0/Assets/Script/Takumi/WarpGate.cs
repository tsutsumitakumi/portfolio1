using UnityEngine;

public class WarpGate : MonoBehaviour
{
    // ワープ先の位置
    public Transform warpDestination;

    // ワープを実行するキー
    public KeyCode warpKey = KeyCode.W;

    // ワープを実行する対象のオブジェクト
    public GameObject targetObject;

    private void Update()
    {
        // キーが押されたかどうかを確認
        if (Input.GetKeyDown(warpKey))
        {
            // ターゲットオブジェクトの前でキーが押されたかどうかを確認
            if (IsPlayerInFrontOfGate())
            {
                // ワープを実行
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

        // ターゲットオブジェクトの前にプレイヤーがいるかどうかを確認
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
        // プレイヤーをワープ先の位置に移動させる
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

