using UnityEngine;

public class KillTrigger : Trigger
{
    protected override void OnPlayerEnter(Collider col)
    {
        base.OnPlayerEnter(col);

        PlayerController.instance.Die();
    }
}
