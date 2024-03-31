using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryCheck : MonoBehaviour
{
    private bool recievedPackage = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Planet") && recievedPackage == false)
        {
            if (GameManager.instance.delievery.currentDelivery == null)
            {
                Debug.Log("Package recieved");
                recievedPackage = true;
                GameManager.instance.delievery.GeneratePackage(GameManager.instance.planetGenerator.RandomPlanet());
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {
            recievedPackage = false;
        }
    }
}
