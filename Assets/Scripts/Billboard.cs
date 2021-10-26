using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private bool invert = true;

    void Update()
    {
        transform.LookAt(PlayerController.instance.GetAttachedCamera().transform);
        if(invert) transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
    }
}
