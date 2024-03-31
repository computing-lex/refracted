using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerController Player;
    public ShipController SteeringWheel;
    public ShipCore ShipCore;

    public static GameManager instance;
    public enum PlayerState
    {
        Walking,
        Cutscene,
        Piloting,
        Extra
    };
    public enum ShipState
    {

    };
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

