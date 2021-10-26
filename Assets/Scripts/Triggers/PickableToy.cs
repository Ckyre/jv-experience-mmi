using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableToy : MonoBehaviour
{
    [SerializeField] private int toyID = 1;

    public void OnPlayerPick()
    {
        PlayerController.instance.PickToy(toyID);
        gameObject.SetActive(false);
    }
}
