using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // Start is called before the first frame update

    private Light light;
    private AudioSource source;

    private float flickerDelay = 50;
    private float flickerCounter = 0;

    private float offTime = 0.05f;
    private float offCounter = 0;

    private float flickerHigh;
    private float flickerLow;

    private float deathTimer = 5;

    private bool hasPlayed = false;
    void Start()
    {   
        light = GetComponent<Light>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimer > 0)
        {

            flickerCounter += Time.deltaTime;

            Debug.Log(flickerCounter + ", delay=" + flickerDelay + ", flicker high and low: " + flickerHigh + ", " + flickerLow);
            if (flickerCounter > flickerDelay)
            {
                if (!hasPlayed)
                {
                    source.PlayOneShot(source.clip);
                    hasPlayed = true;
                }
                light.enabled = false;
            }

            if (!light.enabled)
            {
                offCounter += Time.deltaTime;
                if (offCounter > offTime)
                {
                    offCounter = 0;
                    flickerCounter = 0;
                    flickerDelay = Random.Range(flickerLow, flickerHigh);
                    light.enabled = true;
                    hasPlayed = false;
                }
            }

            //this doesn't quite work but it's close enough for now
            if (true)
            {
                if (GameManager.instance.ShipCore.currentFuel > 400)
                {
                    flickerHigh = 30;
                    flickerLow = 20;
                }
                else if (GameManager.instance.ShipCore.currentFuel < 400 && GameManager.instance.ShipCore.currentFuel > 300)
                {
                    if (flickerHigh == 30)
                    {
                        flickerCounter += 10;
                    }
                    flickerHigh = 25;
                    flickerLow = 15;

                }
                else if (GameManager.instance.ShipCore.currentFuel < 300 && GameManager.instance.ShipCore.currentFuel > 200)
                {
                    if (flickerHigh == 25) flickerCounter += 10;
                    flickerHigh = 20;
                    flickerLow = 10;

                }
                else if (GameManager.instance.ShipCore.currentFuel < 200 && GameManager.instance.ShipCore.currentFuel > 100)
                {
                    if (flickerHigh == 20) flickerCounter += 10;
                    flickerHigh = 15;
                    flickerLow = 5;
                }
                else if (GameManager.instance.ShipCore.currentFuel < 100 && GameManager.instance.ShipCore.currentFuel > 50)
                {
                    if (flickerHigh == 15) flickerCounter += 10;
                    flickerHigh = 10;
                    flickerLow = 5;
                }
                else if (GameManager.instance.ShipCore.currentFuel < 50 && GameManager.instance.ShipCore.currentFuel > 0)
                {
                    if (flickerHigh == 10) flickerCounter += 10;
                    flickerHigh = 2;
                    flickerLow = 1;
                }
                else if (GameManager.instance.ShipCore.currentFuel < 0)
                {
                    flickerHigh = 1.5f;
                    flickerLow = 1;
                    deathTimer -= Time.deltaTime;
                    
                }
            }
        }
        else
        {
            light.enabled = false;
        }

    }
}
