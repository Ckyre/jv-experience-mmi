using UnityEngine;

public class HiddingPlaceTrigger : Trigger
{
    [SerializeField] private AudioClip enterExitClip;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponentInChildren<AudioSource>();
    }

    protected override void OnPlayerEnter(Collider col)
    {
        base.OnPlayerEnter(col);

        PlayerController.instance.ActiveBush(false);
        source.PlayOneShot(enterExitClip);
    }

    protected override void OnPlayerExit(Collider col)
    {
        base.OnPlayerExit(col);
     
        source.PlayOneShot(enterExitClip);
    }
}
