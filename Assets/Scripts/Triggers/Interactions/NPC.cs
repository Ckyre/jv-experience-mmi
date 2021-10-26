using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, Actor
{
    [SerializeField] private TextMesh dialogText;
    [Space]
    [SerializeField] private string name;
    [SerializeField] private List<string> dialogLines = new List<string>();
    [SerializeField] private float letterPrintDelay = 0.05f;
    private int currentDialogLine = -1;

    public void NextDialogLine()
    {
        StopCoroutine("DisplayDialogLine");
        dialogText.text = "";

        if (currentDialogLine == dialogLines.Count - 1)
            currentDialogLine = 0;
        else
            currentDialogLine++;

        StartCoroutine(DisplayDialogLine(dialogLines[currentDialogLine]));
    }

    private IEnumerator DisplayDialogLine (string line)
    {
        for(int c = 0; c < line.Length; c++)
        {
            dialogText.text += line[c];
            yield return new WaitForSeconds(letterPrintDelay);
        }
        dialogText.text = line;
    }

    public void Die() { }

}
