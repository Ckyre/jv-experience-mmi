using System.Collections.Generic;
using UnityEngine;

public class TalkInteraction : Interactable
{
    [SerializeField] private string npcName;
    [SerializeField] private List<string> dialogLines = new List<string>();
    [SerializeField] private float printTime = 1.0f;

    private bool inDialog = false;

    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();

        if (!inDialog)
            StartDialog();
        else
            NextLine();
    }

    private void StartDialog()
    {
        inDialog = true;
        DialogManager.instance.StartDialog(printTime, npcName, dialogLines);
    }

    private void NextLine()
    {
        if (!DialogManager.instance.NextDialogLine())
        {
            inDialog = true;
            StartDialog();
        }
    }

    protected override void OnPlayerExit(Collider col)
    {
        base.OnPlayerExit(col);

        inDialog = false;
        DialogManager.instance.StopDialog();
    }
}
