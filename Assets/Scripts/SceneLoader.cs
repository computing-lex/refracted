using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;
using TMPro;


public class SceneLoader : MonoBehaviour
{
    // sorry in advance that this exists idk how else to use unity :3
    [SerializeField] GameObject pauseMenu = null;
    [SerializeField] GameObject settingsMenu = null;

    //TMP_Dropdown if i ever need that again :3
    bool isPaused;
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    

    // for ui resolutions :3
    public void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        Dropdown.OptionDataList resOptions = new Dropdown.OptionDataList();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) 
        {
            Dropdown.OptionData option = new Dropdown.OptionData(resolutions[i].width + "x" + resolutions[i].height);
            resOptions.options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        // turn into strings for this to function correctly (syntax is a bitch)

        resolutionDropdown.AddOptions(resOptions.options);
        resolutionDropdown.value = currentResIndex;

        Debug.Log(resOptions);
        resolutionDropdown.RefreshShownValue();
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

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
            settingsMenu.SetActive(false);
        }
    }


    public AudioMixer audioMixer;
    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volumeMaster", volume);
        Debug.Log(volume);
    }

    public void fullscreenBoolean(Boolean isFullscreen)
    {
        Debug.Log("Going to Fullscreen...");
        Screen.fullScreen = isFullscreen;
    }

    public void quitGame()
    {
        Debug.Log("Quitting Application...");
        Application.Quit();
    }

    // start game - func right
    public void startGame()
    {        
        //CHANGE ONCE SCENES ARE TOGETHER
        //SceneManager.UnloadScene("MainMenu");
        Debug.Log("Starting Game...");
        MoveToScene(1);
    }

    public void openSettings()
    {
        Debug.Log("Opening Settings...");
    }

    public void resumeGame()
    {
        Debug.Log("Resuming Game...");
    }

    public void MoveToScene(int sceneID) {
        SceneManager.LoadScene(sceneID);
    }
}
