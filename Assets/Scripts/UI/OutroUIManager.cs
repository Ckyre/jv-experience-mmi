using System.Collections.Generic;
using UnityEngine;

public class OutroUIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> parts = new List<GameObject>();
    [SerializeField] private AudioSource source;
    private int currentPart = 0;

    public static OutroUIManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Cursor.visible = true;
        NextPart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameManager.instance.inputCodes.interact))
        {
            NextPart();
        }
    }

    public void NextPart()
    {
        if (currentPart >= parts.Count - 1)
        {
            GameManager.instance.LoadMainMenu();
            return;
        }

        if (currentPart > 0)
            parts[currentPart - 1].GetComponent<CanvasGroup>().alpha = 0;

        parts[currentPart].GetComponent<CanvasGroup>().alpha = 1;

        currentPart++;
    }

    public void PlaySound (AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
