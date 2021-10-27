using System.Collections;
using UnityEngine;

public class PickableToyInteraction : Interactable
{
    public enum ToyType { Bear, Rabbit, Frog }
    [SerializeField] private ToyType type;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private bool playAnimation = true;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();
        
        GameManager.instance.PlaySoundOnMobileSource(transform.position, pickupSound);
        if (playAnimation)
            animator.SetTrigger("PickUp");

        PlayerController.instance.PickToy(type);
        StartCoroutine(DelayedUnactive(0.5f));
    }

    private IEnumerator DelayedUnactive (float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
