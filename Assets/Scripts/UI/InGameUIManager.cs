using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;

    [Header("Dialog")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogName, dialogContent;
    [Header("Death")]
    [SerializeField] private Animator deathPanelAnimator;
    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;

    private bool isPaused = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        ShowDialog(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameManager.instance.inputCodes.close))
        {
            if (isPaused)
                OnResumeButton();
            else
                PauseGame();
        }
    }

    // Dialog
    public void ShowDialog (bool show)
    {
        dialogPanel.SetActive(show);
    }

    public void SetNPCNameText (string name)
    {
        dialogName.text = name;
    }

    public void SetDialogContent (string text)
    {
        dialogContent.text = text;
    }

    // Death panel
    public void ActiveDeathScreen()
    {
        Time.timeScale = 1f;
        deathPanelAnimator.SetTrigger("PlayerDie");
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1f;
        GameManager.instance.RestartGameScene();
    }

    public void OnQuitButton()
    {
        Time.timeScale = 1f;
        GameManager.instance.LoadMainMenu();
    }

    // Pause panel
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void OnResumeButton()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pausePanel.SetActive(false);
    }
}
