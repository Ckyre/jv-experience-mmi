using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image bgImage;
    [SerializeField] private TMP_Text text;

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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DefaultState();
    }

    private void DefaultState()
    {
        bgImage.color = white;

        if(text != null)
            text.color = black;
    }

    private void HoverState()
    {
        bgImage.color = red;

        if (text != null)
            text.color = white;
    }
}
