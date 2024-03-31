using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class ShipCore : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 velocity;
    private Vector3 direction;
    [SerializeField] private Vector3 spawnOffset;
    private bool isLoaded = false;

    // Fuel Values
    public float maxFuel = 1000;
    public float currentFuel;
    private bool recievedPackage = false;
    // UI
    private bool shutdownSound = false;

    [SerializeField] AudioClip breathing;
    [SerializeField] TextMeshProUGUI thingy;
    [SerializeField] TextMeshProUGUI fuelText;

    public TMP_Text location;

    [SerializeField] AudioSource hum;
    [SerializeField] AudioSource losingit;


    public bool inOrbit = false;

    private float orbitTimer = 0;
    private float orbitEvilTime;


    private bool stuff = false;


    void Start()
    {
        isLoaded = false;
        StartCoroutine(WaitForPlanets());
    }

    IEnumerator WaitForPlanets()
    {
        yield return new WaitUntil(() => GameManager.instance.planetGenerator.generationComplete);

        currentFuel = Random.Range(0.5f, 0.6f) * maxFuel; //start with 500-600 fuel

        Vector3 newPosition = GameManager.instance.planetGenerator.planets[Random.Range(0, GameManager.instance.planetGenerator.planets.Count)].transform.position;
        newPosition += spawnOffset;

        transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);

        GameManager.instance.Player.MoveTo(newPosition);
        isLoaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        //isLoaded = true;
        if (isLoaded)
        {
            if (inOrbit && currentFuel > 0)
            {

                orbitTimer += Time.deltaTime;
                if (orbitTimer > orbitEvilTime)
                {
                    GameManager.instance.monster.Pinged(transform.position);
                }
                if (currentFuel < (maxFuel / 2) + 100 && velocity.magnitude < 1)
                {
                    currentFuel += Time.deltaTime * 10f;

                }
            }

            fuelText.text = "Fuel: " + (int)currentFuel;
            var fuck = true;

            //DO NOT ROTATE THE SHIP WHEN PLAYER IS NOT PILOTING BTW


            //Debug.Log(GameManager.instance.Player.GetState() +", " +GameManager.PlayerState.Piloting+", " + currentFuel);

            if (GameManager.instance.Player.GetState() == GameManager.PlayerState.Piloting && currentFuel > 0)
            {
                //GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);

                var previousVel = velocity;
                var previousDir = direction;

                velocity += new Vector3(0, 0, GameManager.instance.Player.GetPlayerInput().y * 10 * Time.deltaTime);
                direction += new Vector3(0, GameManager.instance.Player.GetPlayerInput().x * 15 * Time.deltaTime, 0);

                if (GameManager.instance.Player.GetPlayerInput().magnitude > 0)
                {
                    hum.volume = 1;
                }
                else
                {
                    hum.volume = 0;
                }

                if (velocity.magnitude > 20 && velocity.magnitude > previousVel.magnitude) velocity = previousVel;
                if (direction.magnitude > 14 && direction.magnitude > previousDir.magnitude) direction = previousDir;

                currentFuel -= GameManager.instance.Player.GetPlayerInput().magnitude * Time.deltaTime * 30;

                //Debug.Log(GameManager.instance.Player.GetPlayerInput());

                //Debug.Log(velocity);


                if (velocity.magnitude < 0.1f) velocity = Vector3.zero;

            }

            if (currentFuel < 0 && !shutdownSound)
            {
                hum.volume = 0;
                losingit.PlayOneShot(losingit.clip);
                shutdownSound = true;
            }


            Debug.DrawRay(transform.position + new Vector3(0, 1, 0), velocity, Color.blue);


            //velocity = Vector3.MoveTowards(velocity, Vector3.zero, Time.deltaTime * 2);
            direction = Vector3.MoveTowards(direction, Vector3.zero, Time.deltaTime * 6);

            transform.position += transform.TransformDirection(velocity) * Time.deltaTime; //transform.TransformDirection(velocity * Time.deltaTime);
            transform.Rotate((direction) * Time.deltaTime, Space.Self);

            GameManager.instance.Player.MoveWithShip(transform.TransformDirection(velocity) * Time.deltaTime, direction * Time.deltaTime);

            //Vector3 movementThisFrame = new Vector3(0, 0, 1);
            //Vector3 rotationThisFrame = new Vector3(0, 5, 0);

            //thingy.SetText("Vel: " + (velocity.magnitude).ToString("F1"));
        }

    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IndividualOffset>() != null && isLoaded)
        {
            Debug.Log("Out Of Orbit!");
            inOrbit = false;
            recievedPackage = false;

        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<IndividualOffset>() != null && isLoaded)
        {
            Debug.Log("In Orbit!");
            orbitEvilTime = Random.Range(20, 70);
            inOrbit = true;
            location.text = GameManager.instance.planetGenerator.GetPlanetLoc(other.gameObject.GetComponent<Planet>());
            
            if (GameManager.instance.delievery.hasPackage == false)
            {
                Debug.Log("Package recieved");
                recievedPackage = true;
                GameManager.instance.delievery.GeneratePackage(GameManager.instance.planetGenerator.RandomPlanet());
            }
        }
    }
}
