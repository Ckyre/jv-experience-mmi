using UnityEngine;

public class WalkPlayerState : PlayerState
{
    protected Vector2 moveInputs;

    public override void OnAttach(PlayerController player)
    {
        base.OnAttach(player);

        attachedCamera.SetZoom(parent.properties.cameraZoom);
        parent.GetAnimator().SetBool("isCrouched", false);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // Transitions
        if (Input.GetKeyDown(parent.inputCodes.crouch))
            parent.SetState(new CrouchPlayerState());

        bool isMoving = moveInputs != Vector2.zero;
        parent.GetAnimator().SetBool("isMoving", isMoving);
        if (isMoving)
            parent.OnMoveInvoke();

        Move(parent.properties.walkSpeed);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnDettach()
    {
        base.OnDettach();
    }

    public void Move (float speed)
    {
        moveInputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Rotation toward camera
        if (moveInputs != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.Euler(parent.transform.eulerAngles.x, attachedCamera.transform.eulerAngles.y, parent.transform.eulerAngles.z);
            parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, targetRotation, Time.deltaTime * parent.properties.rotateSpeed);
        }

        // Movement
        Vector3 right = moveInputs.x * attachedCamera.transform.right;
        Vector3 forward = moveInputs.y * attachedCamera.transform.forward;
        Vector3 moveDirection = (right + forward).normalized;

        // Apply move force
        Vector3 moveStep = new Vector3(moveDirection.x * speed * Time.fixedDeltaTime, 0, moveDirection.z * speed * Time.fixedDeltaTime);
        if (parent.GetIsParentedToElevator())
            parent.transform.localPosition += moveStep / 15;
        else
            rb.MovePosition(parent.transform.position + moveStep);
    }
}
