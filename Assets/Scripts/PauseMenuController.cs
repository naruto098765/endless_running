using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingPanel;
    private bool isPaused = false;



    void Start()
    {
        // Make sure the pausePanel is initially inactive
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }


    public void panels()
    {
        settingPanel.SetActive(false);
    }
    void Update()
    {
        // Check for input to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }



    public void OnPauseButtonClick()
    {
        TogglePause();
    }

    public void OnResumeButtonClick()
    {
        ResumeGame();
    }

    public void OnSettingButtonClick()
    {
        Setting();
    }


    void TogglePause()
    {
        isPaused = !isPaused;

        // Pause or resume the game
        Time.timeScale = isPaused ? 0 : 1;

        // Show or hide the pausePanel
        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }
    }

    void ResumeGame()
    {
        isPaused = false;

        // Resume the game
        Time.timeScale = 1;

        // Hide the pausePanel
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    void Setting()
    {
        panels();
        settingPanel.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
