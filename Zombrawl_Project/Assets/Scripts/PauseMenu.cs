using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    private bool isPaused = false;

    
    [SerializeField] private Button homeButton;
    [SerializeField] private Button resumeButton;
    private void OnEnable()
    {
        Time.timeScale = 0f;
        isPaused = true;

        resumeButton.onClick.AddListener(TogglePause);
        homeButton.onClick.AddListener(Home);
    }

    private void Home()
    {
        SceneManager.LoadScene("Menu");
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }
}
