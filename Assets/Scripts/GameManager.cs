using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameWinUi;
    [SerializeField] private GameObject pauseUi; // UI cho Pause Menu

    private bool isGameOver = false;
    private bool isGameWin = false;
    private bool isPaused = false;

    void Start()
    {
        UpdateScore();
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);
        pauseUi.SetActive(false);
    }

    void Update()
    {
        // Nhấn ESC để bật/tắt Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver && !isGameWin)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void AddScore(int points)
    {
        if (!isGameOver && !isGameWin)
        {
            score += points;
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }

    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
    }

    public void Restart()
    {
        isGameOver = false;
        isGameWin = false;
        score = 0;
        UpdateScore();
        Time.timeScale = 1;
        SceneManager.LoadScene("Game"); // nhớ đúng tên scene
    }

    public void Menu()
    {
        isGameOver = false;
        isGameWin = false;
        score = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseUi.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseUi.SetActive(false);
    }

    public bool IsGameOver() => isGameOver;
    public bool IsGameWin() => isGameWin;
    public bool IsPaused() => isPaused;
}
