using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public AudioSource PlayGameAudio;
    //public static AudioSource TempAudio;

    private void Awake()
    {
        //if (TempAudio == null)
        //{
        //    TempAudio = PlayGameAudio;
        //}

        //else
        //{
        //    Destroy(TempAudio);
        //    return;
        //}

        DontDestroyOnLoad(PlayGameAudio);
    }

    private void Update()
    {
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void OptionsToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void Audio()
    {
        PlayGameAudio.Play();
    }
}