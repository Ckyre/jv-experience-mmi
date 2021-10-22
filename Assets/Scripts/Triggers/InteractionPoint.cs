using UnityEngine;
using UnityEngine.Events;

public class InteractionPoint : MonoBehaviour
{
    [SerializeField, Range(0, 200)] private float maxAngleToInteract = 60.0f;
    [SerializeField] private UnityEvent playerInteract;

    private bool playerInTrigger = false;

    private void OnPlayerInteract()
    {
        if (playerInTrigger)
        {
            Vector3 dir = (transform.position - PlayerController.instance.transform.position).normalized;
            float angle = Vector3.Angle(dir, PlayerController.instance.transform.forward);

            if(angle < maxAngleToInteract)
                playerInteract.Invoke();
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if(other.transform == PlayerController.instance.transform)
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.transform == PlayerController.instance.transform)
        {
            playerInTrigger = false;
        }
    }

    private void Start()
    {
        PlayerController.instance.OnInteract += OnPlayerInteract;
    }
}
