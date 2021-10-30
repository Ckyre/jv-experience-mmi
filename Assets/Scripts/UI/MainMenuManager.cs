using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region Singleton
    public static MainMenuManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] private SectionUI mainSection, settingsSection, creditSection, playSection;
    [SerializeField] private Slider musicVolume, sfxVolume;
    [SerializeField] private TMP_Text hightPerformanceButtonText;
    [SerializeField] private AudioSource uiSource, musicSource;

    private SectionUI currentSection;
    private AsyncOperation gameSceneOperation;
    private float startUISourceVolume, startMusicSourceVolume;

    private void Start()
    {
        Cursor.visible = true;

        startUISourceVolume = uiSource.volume;
        startMusicSourceVolume = musicSource.volume;

        SetSection(mainSection);

        // Load game scene async
        gameSceneOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        gameSceneOperation.allowSceneActivation = false;

        // Update UI with user settings
        musicVolume.value = GameManager.gameData.musicVolume;
        sfxVolume.value = GameManager.gameData.sfxVolume;

        // Toggle text update
        if (GameManager.gameData.highPerformance)
        {
            hightPerformanceButtonText.text = "Haute performance : ON";
        }
        else
        {
            hightPerformanceButtonText.text = "Haute performance : OFF";
        }
    }

    public void SetSection (SectionUI nextSection)
    {
        if(currentSection != null)
            currentSection.Active(false);

        currentSection = nextSection;
        currentSection.Active(true);

        if(nextSection == mainSection)
        {
            foreach(ButtonUI btn in mainSection.GetComponentsInChildren<ButtonUI>())
            {
                btn.DefaultState();
            }
        }
    }

    public void StartGame()
    {
        gameSceneOperation.allowSceneActivation = true;
    }

    public void OnStartButton()
    {
        SetSection(playSection);
    }

    public void OnSettingsButton()
    {
        SetSection(settingsSection);
    }

    public void OnCreditsButton()
    {
        SetSection(creditSection);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    // Settings
    public void OnMusicVolumeSlider()
    {
        Debug.Log("start");
        GameManager.gameData.musicVolume = musicVolume.value;
        // Apply to scene
        musicSource.volume = startMusicSourceVolume * GameManager.gameData.musicVolume;
    }

    public void OnSFXVolumeSlider()
    {
        GameManager.gameData.sfxVolume = sfxVolume.value;
        // Apply to scene
        uiSource.volume = startUISourceVolume * GameManager.gameData.sfxVolume;
    }

    public void OnHighPerformanceToggle()
    {
        GameManager.gameData.highPerformance = !GameManager.gameData.highPerformance;

        // Toggle text update
        if (GameManager.gameData.highPerformance)
        {
            hightPerformanceButtonText.text = "Haute performance : ON";
        }
        else
        {
            hightPerformanceButtonText.text = "Haute performance : OFF";
        }
    }

    public AudioSource GetAudioSource()
    {
        return uiSource;
    }
}
