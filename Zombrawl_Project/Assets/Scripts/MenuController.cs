 using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Text scoreText;

    void Start()
    {
        PlayerPrefs.SetInt("Coins", 100);
        startButton.onClick.AddListener(StartGame);
        scoreText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
