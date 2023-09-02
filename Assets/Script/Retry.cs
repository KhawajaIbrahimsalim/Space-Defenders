using UnityEngine.SceneManagement;
using UnityEngine;

public class Retry : MonoBehaviour
{
    public AudioSource RetryAudio;

    private void Start()
    {
        DontDestroyOnLoad(RetryAudio);
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
}
