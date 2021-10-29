using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameData = new GameData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public PlayerInputMapping inputCodes;
    public static GameData gameData;
    [Space]
    [SerializeField] private GameObject mobileSourcePrefab;
    [Header("UI")]
    public AudioClip uiClickSound;
    public AudioClip uiHoverSound;

    public void PlaySoundOnMobileSource (Vector3 position, AudioClip clip)
    {
        GameObject mobileSource = Instantiate(mobileSourcePrefab, position, Quaternion.identity);
        mobileSource.GetComponent<AudioSource>().PlayOneShot(clip);
        Destroy(mobileSource, clip.length);
    }

    public void ApplySettings ()
    {
        foreach (AudioSource source in FindObjectsOfType<AudioSource>())
        {
            // Set volumes
            if (source.tag == "MusicAudioSource")
                source.volume *= gameData.musicVolume;
            else // SFXAudioSource
                source.volume *= gameData.sfxVolume;
        }
    }

    // Load scenes
    private void OnLevelWasLoaded(int level)
    {
        ApplySettings();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadEndGame()
    {
        SceneManager.LoadScene(2);
    }
}
