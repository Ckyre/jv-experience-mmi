using UnityEngine;

public class PickableToyInteraction : Interactable
{
    public enum ToyType { Bear, Rabbit, Frog }
    [SerializeField] private ToyType type;
    [SerializeField] private AudioClip pickupSound;

    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();

        GameManager.instance.PlaySoundOnMobileSource(transform.position, pickupSound);
        PlayerController.instance.PickToy(type);
        gameObject.SetActive(false);
    }
}
