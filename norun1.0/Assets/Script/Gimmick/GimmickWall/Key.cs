using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // プレイヤーがカギを取った場合
            collision.GetComponent<Player>().HasKey = true;
            Destroy(gameObject); // カギを削除
            Debug.Log(collision.GetComponent<Player>().HasKey);
        }
    }
}

