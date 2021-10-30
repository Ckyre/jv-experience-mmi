using UnityEngine;

public class PlaySoundTrigger : Trigger
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool playOneTime = false;
    [SerializeField] private bool isMusic = false;
    [SerializeField] private bool stopOnExit = false;
    
    private AudioSource source;
    private bool aleradyPlayed = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    protected override void OnPlayerEnter(Collider col)
    {
        base.OnPlayerEnter(col);

        if (aleradyPlayed && playOneTime)
            return;

        if (!isMusic)
            source.PlayOneShot(clip);
        else
            PlayerController.instance.PlayMusic(clip, clip.name);

        aleradyPlayed = true;
    }

    protected override void OnPlayerExit(Collider col)
    {
        base.OnPlayerExit(col);

        if (stopOnExit)
        {
            if (!isMusic)
                source.Stop();
            else
                PlayerController.instance.StopMusic();
        }
    }
}
