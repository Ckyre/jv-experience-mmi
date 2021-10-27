using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private List<AudioClip> footstepsSounds = new List<AudioClip>();

    // Animator events
    public void AnimatorPlayFootstep()
    {
        footstepSource.PlayOneShot(footstepsSounds[Random.Range(0, footstepsSounds.Count)]);
        footstepSource.pitch = Random.Range(0.8f, 1.2f);
    }
}
