using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �v���C���[���J�M��������ꍇ
            collision.GetComponent<Player>().HasKey = true;
            Destroy(gameObject); // �J�M���폜
            Debug.Log(collision.GetComponent<Player>().HasKey);
        }
    }
}

