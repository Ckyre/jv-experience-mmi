using UnityEngine;

public class CrouchPlayerState : WalkPlayerState
{
    public override void OnAttach(PlayerController player)
    {
        base.OnAttach(player);

        parent.OnCrouchInvoke();
        attachedCamera.SetZoom(parent.properties.crouchCameraZoom);
        parent.GetAnimator().SetBool("isCrouched", true);
        parent.PlayCrouchSound();
    }

    public override void OnDettach()
    {
        base.OnDettach();
        parent.SetIsHidden(false);
    }

    public override void OnUpdate()
    {
        // Transitions
        if (Input.GetKeyDown(GameManager.instance.inputCodes.crouch))
            parent.SetState(new WalkPlayerState());

        // Animations
        bool isMoving = moveInputs != Vector2.zero;
        parent.GetAnimator().SetBool("isMoving", isMoving);

        if(isMoving) parent.OnMoveInvoke();
        Move(parent.properties.crouchWalkSpeed);
    }

    // Tall grass
    public override void OnStayTrigger (Collider other)
    {
        base.OnEnterTrigger(other);

        if(other.GetComponent<HiddingPlaceTrigger>() != null)
        {
            parent.SetIsHidden(true);
        }
    }

    public override void OnExitTrigger (Collider other)
    {
        base.OnExitTrigger(other);

        if (other.GetComponent<HiddingPlaceTrigger>() != null)
        {
            parent.SetIsHidden(false);
        }
    }
}
