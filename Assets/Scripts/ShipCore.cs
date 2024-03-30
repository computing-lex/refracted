using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCore : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 velocity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DO NOT ROTATE THE SHIP WHEN PLAYER IS NOT PILOTING BTW
        Vector3.MoveTowards(velocity, Vector2.zero, Time.deltaTime* 0.01f);
        if (GameManager.instance.Player.GetState() == GameManager.PlayerState.Piloting)
        {
            velocity+= transform.TransformDirection(new Vector3(GameManager.instance.Player.GetPlayerInput().x,0,GameManager.instance.Player.GetPlayerInput().y));
        }
        //Vector3 movementThisFrame = new Vector3(0, 0, 1);
        //Vector3 rotationThisFrame = new Vector3(0, 5, 0);
        //transform.position += movementThisFrame*Time.deltaTime;
        //transform.Rotate(rotationThisFrame * Time.deltaTime, Space.Self);
       // GameManager.instance.Player.MoveWithShip(movementThisFrame*Time.deltaTime,rotationThisFrame * Time.deltaTime);


    }
}
