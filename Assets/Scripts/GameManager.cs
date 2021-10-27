using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public PlayerInputMapping inputCodes;
    [Space]
    [SerializeField] private GameObject mobileSourcePrefab;

    public void PlaySoundOnMobileSource (Vector3 position, AudioClip clip)
    {
        GameObject mobileSource = Instantiate(mobileSourcePrefab, position, Quaternion.identity);
        mobileSource.GetComponent<AudioSource>().PlayOneShot(clip);
        Destroy(mobileSource, clip.length);
    }

    // Load scenes
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
