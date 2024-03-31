using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI bastard;
    public void quitGame()
    {
        Debug.Log("Quitting Application...");
        Application.Quit();
    }

    private void Start()
    {
        int result = Random.Range(0, 3); 

        switch (result)
        {
            case 0:
                bastard.SetText("Avoid its gaze and stay in the dark.");
                break;
            case 1:
                bastard.SetText("Don't sit still when it notices you.");
                break;
            case 2:
                bastard.SetText("Orbit isn't a safe place to rest.");
                break;
        }
    }

}
