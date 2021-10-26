using UnityEngine;
using UnityEngine.Events;

public class CustomInteraction : Interactable
{
    [SerializeField] private UnityEvent onPlayerInteract;

    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();

        if (onPlayerInteract != null)
            onPlayerInteract.Invoke();
    }
}
