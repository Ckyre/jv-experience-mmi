public class PickableBushInteraction : Interactable
{
    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();

        PlayerController.instance.ActiveBush(true);
        gameObject.SetActive(false);
    }
}
