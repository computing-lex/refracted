using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCore : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 velocity;
    private Vector3 direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DO NOT ROTATE THE SHIP WHEN PLAYER IS NOT PILOTING BTW
        velocity = Vector3.MoveTowards(velocity, Vector3.zero, Time.deltaTime* 2f);
        direction = Vector3.MoveTowards(direction, Vector3.zero, Time.deltaTime * 12f);
        if (GameManager.instance.Player.GetState() == GameManager.PlayerState.Piloting)
        {
            Vector3 prevVel = velocity;
            velocity+= transform.TransformDirection(new Vector3(0,0,GameManager.instance.Player.GetPlayerInput().y))/100;
            if (velocity.magnitude > 10 && velocity.magnitude > prevVel.magnitude) velocity = prevVel;
            direction += transform.TransformDirection(new Vector3(0, GameManager.instance.Player.GetPlayerInput().x, 0))/10;

        }
        Debug.DrawRay(transform.position+new Vector3(0,1,0), velocity, Color.blue);

        transform.position += velocity*Time.deltaTime;
        transform.Rotate(direction * Time.deltaTime, Space.Self);
         GameManager.instance.Player.MoveWithShip(velocity * Time.deltaTime, direction * Time.deltaTime);
        //Vector3 movementThisFrame = new Vector3(0, 0, 1);
        //Vector3 rotationThisFrame = new Vector3(0, 5, 0);



    }

    
}
