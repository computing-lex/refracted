using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidFuckingKillzoneScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool ISBROHERE;

    float a = 0;
    float stamp = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        a += Time.deltaTime;
        if (a > stamp)
        {
            ISBROHERE = false;
            a = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            //Debug.Log("KILLING MYSELF");
            ISBROHERE = true;

            //Debug.Log(ISBROHERE);
        }
       

    }
}
