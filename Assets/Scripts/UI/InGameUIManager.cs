using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private TMP_Text deathMessage;
    [SerializeField] private Button restartButton;

    private AudioSource uiAudioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        ShowDialog(false);
    }

    private void Start()
    {
        uiAudioSource = Camera.main.GetComponentInChildren<AudioSource>();
        restartButton.gameObject.SetActive(false);
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

    public AudioSource GetUIAudioSource()
    {
        return uiAudioSource;
    }

    // Death panel
    public void ActiveDeathScreen()
    {
        deathPanelAnimator.SetTrigger("PlayerDie");
        StartCoroutine(DeathMessageAnimation());
    }

    private IEnumerator DeathMessageAnimation()
    {
        deathMessage.text = "";
        string message = "Vous êtes mort...";
        float delay = 0.0f;
        for(int c = 0; c < message.Length; c++)
        {
            deathMessage.text += message[c];

            if (c > message.Length - 4) delay = 0.5f;
            yield return new WaitForSeconds(0.15f + delay);
        }
        deathMessage.text = message;

        yield return new WaitForSeconds(0.5f);
        restartButton.gameObject.SetActive(true);
    }

    public void OnRestartButton()
    {
        GameManager.instance.RestartGameScene();
    }
}
