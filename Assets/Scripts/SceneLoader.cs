using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Application...");
    }

    public void startGame()
    {
        SceneManager.LoadScene("PauseMenu"); //CHANGE ONCE SCENES ARE TOGETHER
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
}
