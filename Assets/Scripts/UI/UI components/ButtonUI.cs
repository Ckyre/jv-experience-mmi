using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image bgImage;
    [SerializeField] private TMP_Text text;
    [SerializeField] private bool primaryIsBlack = false, onlyTextMode = false;

    private static Color black = new Color(0.1803922f, 0.1568628f, 0.1647059f);
    private static Color white = new Color(0.9686275f, 1, 0.9686275f);
    private static Color red = new Color(0.8470588f, 0.2862745f, 0.2862745f);

    private void Start()
    {
        DefaultState();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverState();

        if (MainMenuManager.instance != null)
            MainMenuManager.instance.GetAudioSource().PlayOneShot(GameManager.instance.uiHoverSound);
        else
            PlayerController.instance.GetUIAudioSource().PlayOneShot(GameManager.instance.uiHoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DefaultState();
    }

    public void DefaultState()
    {
        if(bgImage != null)
        {
            if (primaryIsBlack)
                bgImage.color = black;
            else
                bgImage.color = white;
        }

        if(text != null)
            text.color = black;
    }

    private void HoverState()
    {
        if (text != null)
        {
            if(onlyTextMode)
                text.color = red;
            else
                text.color = white;
        }

        if (bgImage != null && !onlyTextMode)
            bgImage.color = red;
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        if (MainMenuManager.instance != null)
            MainMenuManager.instance.GetAudioSource().PlayOneShot(GameManager.instance.uiClickSound);
        else
            PlayerController.instance.GetUIAudioSource().PlayOneShot(GameManager.instance.uiClickSound);
    }
}
