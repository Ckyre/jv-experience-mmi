using UnityEngine;

public class CrouchPlayerState : WalkPlayerState
{
    public override void OnAttach(PlayerController player)
    {
        base.OnAttach(player);

        parent.OnCrouchInvoke();
        attachedCamera.SetZoom(parent.properties.crouchCameraZoom);
        parent.GetAnimator().SetBool("isCrouched", true);
        parent.GetAnimatorEvents().AnimatorCrouch(0.2f);

        parent.GetCollider().height = 1.4f;
        parent.GetCollider().center = new Vector3(0, -0.3f, 0);
    }

    public override void OnDettach()
    {
        base.OnDettach();

        parent.GetAnimatorEvents().AnimatorCrouch(0.2f);

        parent.SetIsHidden(false);
        parent.GetCollider().height = 1.9f;
        parent.GetCollider().center = Vector3.zero;
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
