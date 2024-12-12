using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private Slider healthSlider; //  display player's health
     [SerializeField] private Text survivalTimerText;
    [SerializeField] private Text coinText;
  
    private int coinCount = 0; // Tracks coin amount

    
    [SerializeField] private GameObject gameOverScreen; // "Game Over"screen
    [SerializeField] private GameObject pauseScreen; //  "Pause" screen

   
    [SerializeField] private Button pauseButton; // opens the pause menu

    private void Start()
    {
        // Hide the "Game Over" and "Pause" screens at the start
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);

        // Assign a listener to the pause button to open the pause menu
        pauseButton.onClick.AddListener(OpenPasueMenu);

        // Load initial coin count from PlayerPrefs or default to 100
        coinCount = PlayerPrefs.GetInt("Coins", 100);
        coinText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    private void OpenPasueMenu()
    {
        // Show the pause screen
        pauseScreen.SetActive(true);
    }

    public void UpdateHealth(float currentHealth)
    {
        // Update the health slider 
        healthSlider.value = currentHealth;
    }

    public void UpdateCoins(int value)
    {
        // Increase coin count
        coinCount += value;
        coinText.text = coinCount.ToString();
        PlayerPrefs.SetInt("Coins", coinCount);
    }

    public void UpdateSurvivalTime(float survivalTime)
    {
      
        survivalTimerText.text = FormatTime(survivalTime);
    }

    private string FormatTime(float timeInSeconds)
    {
        // Convert  time into minutes and seconds, and format it as MM:SS
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ActiveGameOver()
    {
        // Show the "Game Over" screen and pause
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
