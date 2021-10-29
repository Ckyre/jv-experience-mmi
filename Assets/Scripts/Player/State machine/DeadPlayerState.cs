using UnityEngine;

public class DeadPlayerState : PlayerState
{
    public override void OnAttach(PlayerController player)
    {
        base.OnAttach(player);
        parent.GetRigidbody().velocity = Vector3.zero;
        parent.GetAnimator().speed = 0;

        Debug.Log("Player died");

        InGameUIManager.instance.ActiveDeathScreen();
        PlayerController.instance.GetAttachedCamera().IsLock(true);
    }
}
