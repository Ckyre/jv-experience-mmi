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

    private SectionUI currentSection;
    private AsyncOperation gameSceneOperation;

    private void Start()
    {
        SetSection(mainSection);

        // Load game scene async
        gameSceneOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        gameSceneOperation.allowSceneActivation = false;
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
        GameManager.gameData.musicVolume = musicVolume.value;
    }

    public void OnSFXVolumeSlider()
    {
        GameManager.gameData.sfxVolume = sfxVolume.value;
    }

    public void OnHighPerformanceToggle()
    {
        GameManager.gameData.highPerformance = !GameManager.gameData.highPerformance;

        if (GameManager.gameData.highPerformance)
        {
            hightPerformanceButtonText.text = "Haute performance : ON";
        }
        else
        {
            hightPerformanceButtonText.text = "Haute performance : OFF";
        }
    }
}
