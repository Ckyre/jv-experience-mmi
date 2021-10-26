using UnityEngine;

public class EndDoorInteraction : Interactable
{
    [SerializeField] private GameObject bear, rabbit, frog;

    private bool bearIsHere = false;
    private bool rabbitIsHere = false;
    private bool frogIsHere = false;

    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();

        if(bearIsHere && rabbitIsHere && frogIsHere)
        {
            OpenDoor();
            return;
        }

        if(!bearIsHere)
        {
            bearIsHere = PlayerController.instance.GetToy(PickableToyInteraction.ToyType.Bear);
            if (bearIsHere)
            {
                PlayerController.instance.PickToy(PickableToyInteraction.ToyType.Bear, false);
                bear.SetActive(true);
            }
        }
        if (!rabbitIsHere)
        {
            rabbitIsHere = PlayerController.instance.GetToy(PickableToyInteraction.ToyType.Rabbit);
            if (rabbitIsHere)
            {
                PlayerController.instance.PickToy(PickableToyInteraction.ToyType.Rabbit, false);
                rabbit.SetActive(true);
            }
        }
        if (!frogIsHere)
        {
            frogIsHere = PlayerController.instance.GetToy(PickableToyInteraction.ToyType.Frog);
            if (frogIsHere)
            {
                PlayerController.instance.PickToy(PickableToyInteraction.ToyType.Frog, false);
                frog.SetActive(true);
            }
        }
    }

    public void OpenDoor()
    {
        gameObject.SetActive(false);
        // play animation
    }
}
