using UnityEngine;

public class PickableToyInteraction : Interactable
{
    public enum ToyType { Bear, Rabbit, Frog }
    [SerializeField] private ToyType type;

    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();

        PlayerController.instance.PickToy((int)type);
        gameObject.SetActive(false);
    }
}
