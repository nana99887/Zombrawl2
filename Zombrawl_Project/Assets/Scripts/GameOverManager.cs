using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button restartButton;


    private void OnEnable()
    {
        TriggerGameOver();

        homeButton.onClick.AddListener(ExitToMenu);
        restartButton.onClick.AddListener(RestartGame);
    }
    public void TriggerGameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0; // Stop the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
