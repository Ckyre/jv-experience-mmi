using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButtonUI : MonoBehaviour
{
    [SerializeField] private GameObject helpContainer;

    public void ToggleHelp()
    {
        helpContainer.SetActive(!helpContainer.activeSelf);
    }
}
