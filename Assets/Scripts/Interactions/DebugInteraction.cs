using UnityEngine;

public class DebugInteraction : Interactable
{
    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();
        Debug.Log("Player interact with " + this);
    }
}
