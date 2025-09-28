using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
            gameManager.AddScore(1);
        }
        else if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            gameManager.GameWin();
        }
        else if (collision.CompareTag("Trap") || collision.CompareTag("Enemy") || collision.CompareTag("Trash"))
        {
            if (!PlayerController.isImmortal)
            {
                gameManager.GameOver();
            }
            else
            {
                Debug.Log("Immortal mode: ignored collision with " + collision.tag);
            }
        }
    }
}
