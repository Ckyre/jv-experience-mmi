using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private List<AudioClip> footstepsSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> waterFootstepsSounds = new List<AudioClip>();

    private bool isInWater = false;

    // Animator events
    public void AnimatorPlayFootstep()
    {
        if (PlayerController.instance.GetIsGrounded())
        {
            if (!isInWater)
                footstepSource.PlayOneShot(footstepsSounds[Random.Range(0, footstepsSounds.Count)]);
            else
                footstepSource.PlayOneShot(waterFootstepsSounds[Random.Range(0, waterFootstepsSounds.Count)]);

            footstepSource.pitch = Random.Range(0.8f, 1.2f);
        }
    }

    public void SetIsInWater (bool value)
    {
        isInWater = value;
    }
}
