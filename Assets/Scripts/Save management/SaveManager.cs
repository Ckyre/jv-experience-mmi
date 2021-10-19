using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public string relativePath;
    private SaveFile file;

    private void Awake()
    {
        Load();
        file.Data.playerName = "GABRIEL";
        file.Data.playerLevel = 44;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Save();
        }
    }

    public void Load()
    {
        file = new SaveFile(Application.dataPath + relativePath);
    }

    public void Save()
    {
        file.Save();
    }
}
