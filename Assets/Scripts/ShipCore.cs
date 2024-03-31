using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipCore : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 velocity;
    private Vector3 direction;

    // Fuel Values
    public float maxFuel = 1000;
    public float currentFuel;

    // UI
    public TextMeshProUGUI fuelText;

    void Start()
    {
        currentFuel = Random.Range(0.5f, 0.6f) * 1000; //start with 500-600 fuel
    }

    // Update is called once per frame
    void Update()
    {
        fuelText.text = "Fuel: " + currentFuel;
        var fuck = true;

        //DO NOT ROTATE THE SHIP WHEN PLAYER IS NOT PILOTING BTW

        velocity = Vector3.MoveTowards(velocity, Vector3.zero, Time.deltaTime * 1);
        direction = Vector3.MoveTowards(direction, Vector3.zero, Time.deltaTime * 10);

        if (GameManager.instance.Player.GetState() == GameManager.PlayerState.Piloting)
        {
            Vector3 prevVel = velocity;
            velocity += transform.TransformDirection(new Vector3(0, 0, GameManager.instance.Player.GetPlayerInput().y)) / 10;

            currentFuel -= GameManager.instance.Player.GetPlayerInput().magnitude / 10;

            if (velocity.magnitude > 10 && velocity.magnitude > prevVel.magnitude) velocity = prevVel;
            direction += transform.TransformDirection(new Vector3(0, GameManager.instance.Player.GetPlayerInput().x, 0)) / 5;

        }

        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), velocity, Color.blue);

        transform.position += velocity * Time.deltaTime;
        transform.Rotate(direction * Time.deltaTime, Space.Self);

        GameManager.instance.Player.MoveWithShip(velocity * Time.deltaTime, direction * Time.deltaTime);

        //Vector3 movementThisFrame = new Vector3(0, 0, 1);
        //Vector3 rotationThisFrame = new Vector3(0, 5, 0);



    }


}
