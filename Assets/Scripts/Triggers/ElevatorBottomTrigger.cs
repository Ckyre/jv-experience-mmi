using UnityEngine;

public class ElevatorBottomTrigger : Trigger
{
    [SerializeField] private Elevator elevator;

    protected override void OnPlayerEnter(Collider col)
    {
        base.OnPlayerEnter(col);

        if (col.transform == PlayerController.instance.transform)
        {
            elevator.Freeze(true);
        }
    }

    protected override void OnPlayerExit(Collider col)
    {
        base.OnPlayerExit(col);

        if (col.transform == PlayerController.instance.transform)
        {
            elevator.Freeze(false);
        }
    }
}
