using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioInteraction : Interactable
{
    [SerializeField] private AudioClip clip;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();

        source.PlayOneShot(clip);
    }
}
