using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlanetBlast : MonoBehaviour
{
    float timeStamp = 5f;
    float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < timeStamp) {
            counter += Time.deltaTime;
        }    
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Planet")) {
            if (counter < timeStamp ) {
                GameManager.instance.planetGenerator.DestroyPlanet(other.GameObject());
            }
            Debug.Log("LAZERBEAMED!!!!!");
        }
    }
}