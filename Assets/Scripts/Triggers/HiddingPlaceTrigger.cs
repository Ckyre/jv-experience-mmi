using UnityEngine;

public class HiddingPlaceTrigger : Trigger
{
    protected override void OnPlayerEnter(Collider col)
    {
        base.OnPlayerEnter(col);

        PlayerController.instance.ActiveBush(false);
    }
}
