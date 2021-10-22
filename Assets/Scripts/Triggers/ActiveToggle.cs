using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveToggle : MonoBehaviour
{
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void Unactive()
    {
        gameObject.SetActive(false);
    }
}
