using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController parent;

    protected Rigidbody rb;
    protected CameraTP attachedCamera;

    public virtual void OnAttach (PlayerController player)
    {
        parent = player;

        rb = parent.GetRigidbody();
        attachedCamera = parent.GetAttachedCamera();
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnFixedUpdate()
    {
    }

    public virtual void OnDettach()
    {
    }

    public virtual void OnEnterTrigger (Collider other)
    {
    }

    public virtual void OnStayTrigger(Collider other)
    {
    }

    public virtual void OnExitTrigger (Collider other)
    {
    }
}
