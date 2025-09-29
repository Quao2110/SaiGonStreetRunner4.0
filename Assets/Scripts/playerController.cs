using UnityEngine;
using TMPro;  // cần import TMP

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    [Header("Fall Settings")]
    [SerializeField] private float fallLimitY = -5f; // giới hạn rơi xuống

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI immortalText; // gán trong Inspector

    private Animator animation;
    private Rigidbody2D rb;
    private bool isGrounded;
    private GameManager gameManager;

    public static bool isImmortal = false;   // 🔹 Đặt static để script khác dùng

    private void Awake()
    {
        animation = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
        UpdateImmortalUI();
    }

    void Update()
    {
        if (gameManager.IsGameOver() || gameManager.IsGameWin()) return;

        HandleMove();
        HandleJump();
        UpdateAnimation();

        // 🔹 Toggle bất tử bằng tổ hợp Alt + Ctrl
        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) &&
            (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)))
        {
            isImmortal = !isImmortal;
            Debug.Log("Live: " + isImmortal);
            UpdateImmortalUI();
        }

        // 🔹 Nếu rơi xuống vực thì thua (trừ khi bất tử)
        if (!isImmortal && transform.position.y < fallLimitY)
        {
            gameManager.GameOver();
        }

        // Debug: Nhấn Alt + Shift để thắng
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            gameManager.GameWin();
        }
    }

    private void HandleMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        if (moveX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJump = !isGrounded;
        animation.SetBool("IsRunning", isRunning);
        animation.SetBool("IsJump", isJump);
    }

    private void UpdateImmortalUI()
    {
        if (immortalText != null)
        {
            if (isImmortal)
            {
                immortalText.gameObject.SetActive(true);   // bật hiện khi bất tử
                immortalText.text = "LIVE";
                immortalText.color = Color.green;
            }
            else
            {
                immortalText.gameObject.SetActive(false);  // tắt khi bình thường
            }
        }
    }

    public void TakeDamage()
    {
        if (!isImmortal)
        {
            gameManager.GameOver();
        }
        else
        {
            Debug.Log("Hit ignored (Immortal mode ON)");
        }
    }
}
