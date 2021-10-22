public class DeadPlayerState : PlayerState
{
    public override void OnAttach(PlayerController player)
    {
        base.OnAttach(player);

        parent.GetAnimator().speed = 0;
    }
}
