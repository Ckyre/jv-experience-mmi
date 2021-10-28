using UnityEngine;

public class WaterAeraTrigger : Trigger
{
    protected override void OnPlayerEnter(Collider col)
    {
        base.OnPlayerEnter(col);

        PlayerController.instance.SetIsInWater(true);
    }

    protected override void OnPlayerExit(Collider col)
    {
        base.OnPlayerExit(col);

        PlayerController.instance.SetIsInWater(false);
    }
}
