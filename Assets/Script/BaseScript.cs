using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BaseScript : MonoBehaviour
{
    public Canvas TutorialScreen;
    public TextMeshProUGUI Paused_Text;
    public Canvas Upgrade_PlayerPanal;
    public Canvas Upgrade_AllyTowerPanal;
    public Canvas PlayerAndAllytowerPanal;
    public Button UpgradeButton;

    private bool isPaused = true;
    private bool isTutorialScreenOn = true;

    // For Game money system
    public float ReactorAmount = 0f;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (!isTutorialScreenOn && Upgrade_PlayerPanal.isActiveAndEnabled == false && Upgrade_AllyTowerPanal.isActiveAndEnabled == false && PlayerAndAllytowerPanal.isActiveAndEnabled == false)
        {
            if (Input.GetKeyDown("escape") && isPaused)
            {
                Paused_Text.gameObject.SetActive(true);
                UpgradeButton.interactable = false;
                Time.timeScale = 0f;
                isPaused = false;
            }

            else if (Input.GetKeyDown("escape") && !isPaused)
            {
                Time.timeScale = 1f;
                Paused_Text.gameObject.SetActive(false);
                UpgradeButton.interactable = true;
                isPaused = true;
            }
        }
    }

    public void CloseTutorialScreen()
    {
        TutorialScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
        isTutorialScreenOn = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ToOptions()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
