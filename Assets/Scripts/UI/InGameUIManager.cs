using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;

    [Header("Dialog")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogName, dialogContent;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        ShowDialog(false);
    }

    // Dialog
    public void ShowDialog (bool show, string npcName = "")
    {
        dialogPanel.SetActive(show);
        if (show) dialogName.text = npcName;
    }

    public void SetDialogContent (string text)
    {
        dialogContent.text = text;
    }
}
