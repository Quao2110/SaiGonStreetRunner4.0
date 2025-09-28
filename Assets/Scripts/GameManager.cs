using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameWinUi;

    private bool isGameOver = false;
    private bool isGameWin = false;

    void Start()
    {
        UpdateScore();
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);
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

    public bool IsGameOver() => isGameOver;
    public bool IsGameWin() => isGameWin;
}
