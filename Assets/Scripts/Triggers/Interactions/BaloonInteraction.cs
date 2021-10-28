using UnityEngine;

public class BaloonInteraction : Interactable
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private GameObject baloon;
    [SerializeField] private GameObject rope;
    [SerializeField] private AudioClip interactClip;

    private AudioSource source;
    private bool baloonFree = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();

        if (!baloonFree)
        {
            baloonFree = true;
            rope.SetActive(false);
            source.PlayOneShot(interactClip);
            Destroy(gameObject, 60f);
        }
    }

    private void Update()
    {
        if (baloonFree)
        {
            baloon.transform.position += direction * speed * Time.deltaTime;
        }
    }
}
