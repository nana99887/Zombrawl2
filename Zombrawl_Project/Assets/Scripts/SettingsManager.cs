using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    public Toggle soundToggle;
    public Toggle musicToggle;

    public Button closeButon;
    private void OnEnable()
    {
        // Load preferences
        bool isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        bool isMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;

        // Set toggle states
        soundToggle.isOn = isSoundEnabled;
        musicToggle.isOn = isMusicEnabled;

        // Assign toggle listeners
        soundToggle.onValueChanged.AddListener(delegate { OnSoundToggleChanged(soundToggle.isOn); });
        musicToggle.onValueChanged.AddListener(delegate { OnMusicToggleChanged(musicToggle.isOn); });

        closeButon.onClick.RemoveAllListeners();
        closeButon.onClick.AddListener(CloseSettings);
    }

    private void OnSoundToggleChanged(bool isOn)
    {
        SoundsManager.Instance.ToggleSound(isOn);
    }

    private void OnMusicToggleChanged(bool isOn)
    {
        SoundsManager.Instance.ToggleMusic(isOn);
    }

    private void CloseSettings()
    {
        gameObject.SetActive(false);
    }
}
