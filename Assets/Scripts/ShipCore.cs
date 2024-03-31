using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipCore : MonoBehaviour
{
    // Start is called before the first frame update
    /*
    private Vector3 velocity;
    private Vector3 direction;*/

    public float maxFuel = 1000;
    public float currentFuel;

    public TextMeshProUGUI textMeshProUGUI;
    void Start()
    {
        currentFuel = Random.Range(0.5f, 0.6f) * 1000; //start with 500-600 fuel
    }

    // Update is called once per frame


    void Update()
    {
        textMeshProUGUI.text = "Fuel: " + currentFuel;
        var fuck = true;
        //DO NOT ROTATE THE SHIP WHEN PLAYER IS NOT PILOTING BTW


        if (GameManager.instance.Player.GetState() == GameManager.PlayerState.Piloting && currentFuel > 0)
        {
            currentFuel -= GameManager.instance.Player.GetPlayerInput().magnitude * Time.deltaTime * 2;

        }
    }
}
          