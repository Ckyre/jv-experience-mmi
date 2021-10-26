using UnityEngine;

public class ActiveGameObjectInteraction : Interactable
{
    public GameObject targetObject;

    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();

        targetObject.SetActive(!targetObject.activeSelf);
    }
}
