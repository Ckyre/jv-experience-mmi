using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private bool showGizmos = true;
    [Space]
    public UnityEvent playerEnter;
    public UnityEvent playerExit;
    public UnityEvent playerStay;

    private void OnTriggerEnter (Collider other)
    {
        if(other.transform == PlayerController.instance.transform)
        {
            playerEnter.Invoke();
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.transform == PlayerController.instance.transform)
        {
            playerExit.Invoke();
        }
    }

    private void OnTriggerStay (Collider other)
    {
        if (other.transform == PlayerController.instance.transform)
        {
            playerStay.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}
