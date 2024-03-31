using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingButton : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Ping!");
        GameManager.instance.Ping();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
