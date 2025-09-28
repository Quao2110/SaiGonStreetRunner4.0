using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float distance = 5f;
    private Vector3 startPos;
    private bool movingRight = true;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if(transform.position.x >= rightBound)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if(transform.position.x <= leftBound)
            {
                movingRight = true;
                Flip();
            }
        }
    } 
    void Flip ()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
