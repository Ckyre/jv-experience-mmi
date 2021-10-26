using UnityEngine;
using UnityEngine.Events;

public class CustomTrigger : Trigger
{
    [SerializeField] private UnityEvent onPlayerEnter;
    [SerializeField] private UnityEvent onPlayerStay;
    [SerializeField] private UnityEvent onPlayerExit;

    protected override void OnPlayerEnter(Collider col)
    {
        base.OnPlayerEnter(col);

        if(col.transform == PlayerController.instance.transform)
        {
            if(onPlayerEnter != null)
                onPlayerEnter.Invoke();
        }
    }

    protected override void OnPlayerStay(Collider col)
    {
        base.OnPlayerStay(col);

        if (col.transform == PlayerController.instance.transform)
        {
            if (onPlayerStay != null)
                onPlayerStay.Invoke();
        }
    }

    protected override void OnPlayerExit(Collider col)
    {
        base.OnPlayerExit(col);

        if (col.transform == PlayerController.instance.transform)
        {
            if (onPlayerExit != null)
                onPlayerExit.Invoke();
        }
    }
}
