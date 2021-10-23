using UnityEngine;

public class PickableBush : MonoBehaviour
{
    private bool picked = false;

    public void Pick()
    {
        if (!picked)
        {
            picked = true;
            PlayerController.instance.ActiveBush(true);

            // Play pick animation

            transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}
