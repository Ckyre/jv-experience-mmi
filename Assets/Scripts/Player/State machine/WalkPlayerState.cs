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

    public void Move (float speed)
    {
        moveInputs = parent.inputCodes.GetMoveAxis();

        // Rotation toward camera
        if (moveInputs != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.Euler(parent.transform.eulerAngles.x, attachedCamera.transform.eulerAngles.y, parent.transform.eulerAngles.z);
            parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, targetRotation, Time.deltaTime * parent.properties.rotateSpeed);
        }

        // Movement
        /*
        Vector3 rotatedGroundNormal = parent.GetGroundNormal();
        Vector3 right = moveInputs.x * (Quaternion.AngleAxis(90, Vector3.forward) * rotatedGroundNormal);
        Vector3 forward = moveInputs.y * (Quaternion.AngleAxis(90, -Vector3.right) * rotatedGroundNormal);
        Vector3 moveDirection = (right + forward).normalized;
        Debug.DrawRay(new Vector3(parent.transform.position.x, parent.transform.position.y + parent.properties.feetPos.y, parent.transform.position.z), moveDirection, Color.red);
        */
        Vector3 right = moveInputs.x * attachedCamera.transform.right;
        Vector3 forward = moveInputs.y * attachedCamera.transform.forward;
        Vector3 moveDirection = (right + forward).normalized;

        // Apply move force
        if (parent.GetIsParentedToElevator())
        {
            Vector3 moveStep = new Vector3(moveDirection.x * 3 * Time.fixedDeltaTime, 0, moveDirection.z * 3 * Time.fixedDeltaTime);
            parent.transform.localPosition += moveStep;
            rb.velocity = Vector3.zero;
        }
        else
        {
            Vector3 velocity = new Vector3(moveDirection.x * speed * Time.fixedDeltaTime, rb.velocity.y, moveDirection.z * speed * Time.fixedDeltaTime);
            rb.velocity = velocity;
            //rb.MovePosition(parent.transform.position + velocity);
        }
    }
}
