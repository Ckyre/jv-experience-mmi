using UnityEngine;

public class CrouchPlayerState : WalkPlayerState
{
    public override void OnAttach(PlayerController player)
    {
        base.OnAttach(player);

        parent.OnCrouchInvoke();
        attachedCamera.SetZoom(parent.properties.crouchCameraZoom);
        parent.GetAnimator().SetBool("isCrouched", true);
    }

    public override void OnUpdate()
    {
        // Transitions
        if (Input.GetKeyDown(parent.inputCodes.crouch))
            parent.SetState(new WalkPlayerState());

        bool isMoving = moveInputs != Vector2.zero;
        parent.GetAnimator().SetBool("isMoving", isMoving);
        if(isMoving)
            parent.OnMoveInvoke();

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
