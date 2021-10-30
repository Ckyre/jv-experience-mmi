using UnityEngine;

public abstract class Interactable : Trigger
{
    [SerializeField, Range(0, 200)] private float maxAngleToInteract = 60.0f;
    private bool playerInTrigger = false;

    public void OnPlayerTryInteract()
    {
        if (playerInTrigger)
        {
            // Get player angle
            Vector3 dir = (transform.position - PlayerController.instance.transform.position).normalized;
            float angle = Vector3.Angle(dir, PlayerController.instance.transform.forward);

            if (angle < maxAngleToInteract || Vector3.Distance(transform.position, PlayerController.instance.transform.position) < 1.2f)
                OnPlayerInteract();
        }
    }

    public virtual void OnPlayerInteract() { }

    // Enter / exit trigger
    protected override void OnPlayerEnter (Collider col)
    {
        base.OnPlayerEnter(col);

        if (col.transform == PlayerController.instance.transform)
        {
            playerInTrigger = true;
            PlayerController.instance.SetInteractionListener(this);
        }
    }

    protected override void OnPlayerStay(Collider col)
    {
        base.OnPlayerStay(col);

        if(col.transform == PlayerController.instance.transform)
        {
            InGameUIManager.instance.SetInteractionUI(true);
        }
    }

    protected override void OnPlayerExit(Collider col)
    {
        base.OnPlayerExit(col);

        if (col.transform == PlayerController.instance.transform)
        {
            playerInTrigger = false;
            InGameUIManager.instance.SetInteractionUI(false);
        }
    }
}
