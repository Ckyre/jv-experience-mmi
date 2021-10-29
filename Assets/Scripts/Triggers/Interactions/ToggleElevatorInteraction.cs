using UnityEngine;

public class ToggleElevatorInteraction : Interactable
{
    [SerializeField] private Elevator elevator;
    [SerializeField] private GameObject leverMesh;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip toggleSound;
    [SerializeField] private bool active = true;

    private bool toggle = true;

    public override void OnPlayerInteract()
    {
        if (active)
        {
            base.OnPlayerInteract();

            elevator.Toggle();
            source.PlayOneShot(toggleSound);

            toggle = !toggle;
            if (toggle)
                leverMesh.transform.eulerAngles += new Vector3(90f, 0, 0);
            else
                leverMesh.transform.eulerAngles -= new Vector3(90f, 0, 0);
        }
    }

    public void Active (bool value)
    {
        active = value;
    }
}
