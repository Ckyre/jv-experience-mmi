using UnityEngine;

public class SectionUI : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private SectionUI backSection;

    private void Start()
    {
        Active(false);
    }

    public void Active (bool active)
    {
        container.SetActive(active);
    }

    public void Quit()
    {
        MainMenuManager.instance.SetSection(backSection);
    }
}
