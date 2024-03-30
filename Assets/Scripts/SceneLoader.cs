using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // sorry in advance that this exists idk how else to use unity
    [SerializeField] GameObject pauseMenu = null;
    [SerializeField] GameObject resumeButton = null;
    [SerializeField] GameObject settingsButton = null;
    [SerializeField] GameObject quitButton = null;

    bool isPaused;

    public void setisPaused(bool settingto)
    {
        isPaused = settingto;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;

            // fuck you ryan dumbass
            pauseMenu.SetActive(isPaused);
            resumeButton.SetActive(isPaused);
            settingsButton.SetActive(isPaused);
            quitButton.SetActive(isPaused);
        }
    }
    
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Application...");
    }

    // start game - func right
    public void startGame()
    {        
        //CHANGE ONCE SCENES ARE TOGETHER
        //SceneManager.UnloadScene("MainMenu");
        Debug.Log("Starting Game...");
    }

    public void openSettings()
    {
        Debug.Log("Opening Settings...");
    }

    public void resumeGame()
    {
        Debug.Log("Resuming Game...");
    }
    public void setVolume(float volume)
    {
        Debug.Log(volume);
    }

    public void fullscreenBoolean()
    {
        Debug.Log("Going to Fullscreen...");
    }
}
