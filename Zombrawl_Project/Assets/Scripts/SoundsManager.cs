using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;

   
    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;

 
    public AudioClip shootSound;
    public AudioClip gameOverSound;
    public AudioClip coinCollectSound;

   
    private bool isSoundEnabled = true;
    private bool isMusicEnabled = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Load preferences
        isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        isMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;

        // Handle background music
        backgroundMusicSource.mute = !isMusicEnabled;
    }

    public void PlaySound(AudioClip clip)
    {
        if (!isSoundEnabled || clip == null) return;

        sfxSource.PlayOneShot(clip);
    }

    public void ToggleSound(bool isEnabled)
    {
        isSoundEnabled = isEnabled;
        PlayerPrefs.SetInt("SoundEnabled", isEnabled ? 1 : 0);
    }

    public void ToggleMusic(bool isEnabled)
    {
        isMusicEnabled = isEnabled;
        backgroundMusicSource.mute = !isEnabled;
        PlayerPrefs.SetInt("MusicEnabled", isEnabled ? 1 : 0);
    }
}
