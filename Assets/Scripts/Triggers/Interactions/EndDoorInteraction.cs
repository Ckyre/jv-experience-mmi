using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoorInteraction : Interactable
{
    [SerializeField] private GameObject bear, rabbit, frog;
    [Space]
    [SerializeField] private AudioClip successAddToySound;
    [SerializeField] private AudioClip failedAddToySound;
    [SerializeField] private AudioClip openDoorSound;

    private bool bearIsHere = false;
    private bool rabbitIsHere = false;
    private bool frogIsHere = false;
    private bool alreadyPlayedASound = false;

    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();

        if(bearIsHere && rabbitIsHere && frogIsHere)
        {
            OpenDoor();
            return;
        }

        alreadyPlayedASound = false;

        if(!bearIsHere)
        {
            bearIsHere = PlayerController.instance.GetToy(PickableToyInteraction.ToyType.Bear);
            if (bearIsHere)
            {
                PlayerController.instance.PickToy(PickableToyInteraction.ToyType.Bear, false);
                bear.SetActive(true);

                if (!alreadyPlayedASound)
                {
                    alreadyPlayedASound = true;
                    GameManager.instance.PlaySoundOnMobileSource(transform.position, successAddToySound);
                }
            }
        }
        if (!rabbitIsHere)
        {
            rabbitIsHere = PlayerController.instance.GetToy(PickableToyInteraction.ToyType.Rabbit);
            if (rabbitIsHere)
            {
                PlayerController.instance.PickToy(PickableToyInteraction.ToyType.Rabbit, false);
                rabbit.SetActive(true);

                if (!alreadyPlayedASound)
                {
                    alreadyPlayedASound = true;
                    GameManager.instance.PlaySoundOnMobileSource(transform.position, successAddToySound);
                }
            }
        }
        if (!frogIsHere)
        {
            frogIsHere = PlayerController.instance.GetToy(PickableToyInteraction.ToyType.Frog);
            if (frogIsHere)
            {
                PlayerController.instance.PickToy(PickableToyInteraction.ToyType.Frog, false);
                frog.SetActive(true);

                if (!alreadyPlayedASound)
                {
                    alreadyPlayedASound = true;
                    GameManager.instance.PlaySoundOnMobileSource(transform.position, successAddToySound);
                }
            }
        }

        if (!alreadyPlayedASound)
        {
            alreadyPlayedASound = true;
            GameManager.instance.PlaySoundOnMobileSource(transform.position, failedAddToySound);
        }
    }

    public void OpenDoor()
    {
        GameManager.instance.PlaySoundOnMobileSource(transform.position, openDoorSound);
        StartCoroutine(DelayedOpenDoor(0.5f));
    }

    private IEnumerator DelayedOpenDoor (float delay)
    {
        yield return new WaitForSeconds(delay);
        //gameObject.SetActive(false);
        SceneManager.LoadScene(2);
    }
}
