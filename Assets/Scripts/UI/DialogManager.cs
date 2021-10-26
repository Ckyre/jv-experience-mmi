using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    #region Singleton
    public static DialogManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    private bool inDialog = false;
    private float printTime;
    private int currentLineIndex = -1;
    private List<string> lines;

    public void StartDialog (float printTime, string npcName, List<string> dialogLines)
    {
        if (InGameUIManager.instance == null) return;

        inDialog = true;

        InGameUIManager.instance.ShowDialog(true);
        InGameUIManager.instance.SetNPCNameText(npcName);

        lines = dialogLines;
        this.printTime = printTime;
        currentLineIndex = -1;
        NextDialogLine();
    }

    public void StopDialog()
    {
        inDialog = false;

        StopCoroutine("DisplayDialogLine");
        InGameUIManager.instance.ShowDialog(false);
        InGameUIManager.instance.SetNPCNameText("");
        InGameUIManager.instance.SetDialogContent("");
    }

    public bool NextDialogLine()
    {
        if (!inDialog) return false;

        if(currentLineIndex < lines.Count - 1)
        {
            StopCoroutine("DisplayDialogLine");
            InGameUIManager.instance.SetDialogContent("");

            currentLineIndex++;
            StartCoroutine(DisplayDialogLine(lines[currentLineIndex]));
        }
        else
        {
            StopDialog();
        }

        return true;
    }

    private IEnumerator DisplayDialogLine(string line)
    {
        string text = "";
        for (int c = 0; c < line.Length; c++)
        {
            text += line[c];
            InGameUIManager.instance.SetDialogContent(text);

            yield return new WaitForSeconds(printTime/line.Length);
        }
        InGameUIManager.instance.SetDialogContent(line);
    }
}
