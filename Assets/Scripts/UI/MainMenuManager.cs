using UnityEngine;
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

    [SerializeField] private SectionUI mainSection, settingsSection, creditSection;
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
    }

    public void OnStartButton()
    {
        gameSceneOperation.allowSceneActivation = true;
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
}
