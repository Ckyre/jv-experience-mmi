using UnityEngine;

public class BaloonInteraction : Interactable
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private GameObject baloon;
    [SerializeField] private GameObject rope;

    private bool baloonFree = false;

    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();

        rope.SetActive(false);
        baloonFree = true;
        Destroy(gameObject, 60f);
    }

    private void Update()
    {
        if (baloonFree)
        {
            baloon.transform.position += direction * speed * Time.deltaTime;
        }
    }
}
