using UnityEngine;

public class ElevatorBottomTrigger : MonoBehaviour
{
    [SerializeField] private Elevator elevator;

    private void OnTriggerEnter (Collider other)
    {
        if(other.transform == PlayerController.instance.transform)
        {
            elevator.Freeze(true);
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.transform == PlayerController.instance.transform)
        {
            elevator.Freeze(false);
        }
    }
}
