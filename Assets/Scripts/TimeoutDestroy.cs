using UnityEngine;

public class TimeoutDestroy : MonoBehaviour
{
    [SerializeField] private float delay = 1.0f;
    [SerializeField] private bool atStart = false;

    private void Start()
    {
        if (atStart)
        {
            StartDestroying();
        }
    }

    public void StartDestroying()
    {
        Destroy(gameObject, delay);
    }
}
