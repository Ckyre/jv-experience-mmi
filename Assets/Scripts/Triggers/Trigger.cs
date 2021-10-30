using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Trigger : MonoBehaviour
{
    protected virtual void OnPlayerEnter (Collider col) { }
    protected virtual void OnPlayerStay (Collider col) { }
    protected virtual void OnPlayerExit (Collider col) { }

    private void OnTriggerEnter(Collider other)
    {
        OnPlayerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnPlayerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnPlayerExit(other);
    }
}
