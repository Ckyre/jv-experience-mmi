using UnityEngine;

public class CrouchPlayerState : WalkPlayerState
{
    public override void OnAttach(PlayerController player)
    {
        base.OnAttach(player);

        attachedCamera.SetZoom(parent.properties.crouchCameraZoom);
        parent.GetAnimator().SetBool("isCrouched", true);
    }

    public override void OnUpdate()
    {
        // Transitions
        if (Input.GetKeyDown(KeyCode.C))
            parent.SetState(new WalkPlayerState());

        parent.GetAnimator().SetBool("isMoving", moveInputs != Vector2.zero);

        Move(parent.properties.crouchWalkSpeed);
    }

    public override void OnEnterTrigger (Collider other)
    {
        base.OnEnterTrigger(other);

        if(other.GetComponent<HiddingPlace>() != null)
        {
            parent.SetIsHidden(true);
        }
    }

    public override void OnExitTrigger (Collider other)
    {
        base.OnExitTrigger(other);

        if (other.GetComponent<HiddingPlace>() != null)
        {
            parent.SetIsHidden(false);
        }
    }
}
